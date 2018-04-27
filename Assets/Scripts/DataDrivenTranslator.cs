using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataDriven
{
    public class DataDrivenTranslator
    {
        /// <summary>
        /// 驱动器名称
        /// </summary>
        static private string drivenName = string.Empty;
        /// <summary>
        /// 驱动解析器
        /// </summary>
        static BaseDrivenTranslator baseTranslator = null;
        /// <summary>
        /// 解析器栈
        /// </summary>
        static Stack<BaseDrivenTranslator> translatorStack = new Stack<BaseDrivenTranslator>();
        /// <summary>
        /// 解析驱动
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>驱动</returns>
        public static BaseDriven Translate(string content)
        {
            translatorStack.Clear();
            baseTranslator = null;
            string[] lines = content.Split('\n');
            int length = lines.Length;
            for (int i = 0; i < length; i++)
            {
                ParseLine(lines[i]);
            }
            if (baseTranslator == null)
                return null;
            object obj = baseTranslator.GetObject();
            if (obj != null) {
                return obj as BaseDriven;
            }
            return null;
        }
        /// <summary>
        /// 解析行内容
        /// </summary>
        /// <param name="line">内容</param>
        private static void ParseLine(string line)
        {
            // 去掉空符号
            string _content = line.Replace(" ", "");
            _content = line.Replace("\t", "");
            // 丢弃//后的内容
            int index = _content.IndexOf("//");
            if (index == 0)
            {
                // 整行丢弃
                return;
            }
            if (index != -1)
            {
                // 需要丢弃
                _content = _content.Substring(0, index);
            }
            TranslateLine(_content.Replace("\r", ""));
        }
        /// <summary>
        /// 生成每一行的解析器
        /// </summary>
        /// <param name="content">内容</param>
        private static void TranslateLine(string content)
        {
            string[] keyValue = content.Split(new string[] { "\"\"" }, StringSplitOptions.RemoveEmptyEntries);
			if (keyValue.Length == 0)
				return;
            if (keyValue.Length == 1)
            {
                // 非key-value结构
                TranslateNonKeyValue(keyValue[0].Replace("\"", ""));
            }
            else
            {
                TranslateKeyValue(keyValue[0].Replace("\"", ""), keyValue[1].Replace("\"", ""));
            }
        }
        /// <summary>
        /// 解析非key-value行数据
        /// </summary>
        /// <param name="content">内容</param>
        private static void TranslateNonKeyValue(string content)
        {
            if (baseTranslator == null)
            {
                // 去掉第一个｛
                if (content == "{")
                    return;
                // 什么都没有的情况下，只记录驱动名称
                // 除了驱动名称外，第一个应该声明驱动的类型
                drivenName = content;
            }
            else
            {
                if (content == "}")
                {
                    // 完成一个解析器的解析
                    if (translatorStack.Count > 0)
                    {
                        // 添加到父节点
                        BaseDrivenTranslator _base = translatorStack.Pop();
                        _base.AddTranslator(baseTranslator);
                        baseTranslator = _base;
                    }
                    else
                    {
                        // 解析完成
                        return;

                    }
                }
                if (System.Enum.IsDefined(typeof(DataDrivenConfig.AbilityEventName), content)){
                    DataDrivenConfig.AbilityEventName eventName = (DataDrivenConfig.AbilityEventName)System.Enum.Parse(typeof(DataDrivenConfig.AbilityEventName), content);
                    TranslateEventValue(eventName);
                }
            }
        }
        private static void TranslateEventValue(DataDrivenConfig.AbilityEventName eventName) {
            switch (eventName) {
                case DataDrivenConfig.AbilityEventName.OnSpellStart:
                    // 解析驱动对应的驱动
                    var _baseTranslator = CreateEventTranslator(eventName);
                    HasCreateNewTranslator(_baseTranslator);
                    break;
                case DataDrivenConfig.AbilityEventName.OnProjecticleHit:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 解析key-value行数据
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        private static void TranslateKeyValue(string key, string value)
        {
            switch (key)
            {
                case DrivenPropertyConst.ABILITYNAME:
					{
						// 解析驱动对应的驱动
						var _baseTranslator = CreateDrivenTranslator(value);
                        HasCreateNewTranslator(_baseTranslator);
                    }
                    break;
                default:
                    baseTranslator.AddKeyValue(key, value);
                    break;
            }
        }
        /// <summary>
        /// 添加解析器
        /// </summary>
        /// <param name="_baseTranslator">解析器</param>
        private static void HasCreateNewTranslator(BaseDrivenTranslator _baseTranslator)
        {
            if (baseTranslator != null) {
                translatorStack.Push(baseTranslator);
            }
            baseTranslator = _baseTranslator;
        }

        /// <summary>
        /// 生成相应的驱动解析器
        /// </summary>
        /// <param name="aname">驱动名</param>
        /// <returns>解析器</returns>
        public static BaseDrivenTranslator CreateDrivenTranslator(string aname) {
			BaseDrivenTranslator _translator = null;
			switch (aname) {
                case DrivenConst.BUILD_DRIVEN:
                    _translator = new BuildDrivenTranslator(drivenName);
                    break;
                case DrivenConst.ABILITY_DRIVEN:
                    _translator = new AbilityDrivenTranslator(drivenName);
                    break;
				case DrivenConst.BASE_DRIVEN:
				default:
					_translator = new BaseDrivenTranslator(drivenName);
					break;
			}
			return _translator;
		}
        /// <summary>
        /// 生成相应的事件解析器
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <returns>解析器</returns>
        public static BaseDrivenTranslator CreateEventTranslator(DataDrivenConfig.AbilityEventName eventName) {
            BaseDrivenTranslator _translator = null;
            _translator = new AbilityEventTranslator(eventName.ToString());
            return _translator;
        }
    }
}
