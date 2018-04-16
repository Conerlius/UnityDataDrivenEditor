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
        /// 解析驱动
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>驱动</returns>
        public static BaseAbility Translate(string content)
        {
            baseTranslator = null;
            string[] lines = content.Split('\n');
            int length = lines.Length;
            for (int i = 0; i < length; i++)
            {
                ParseLine(lines[i]);
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
            TranslateLine(_content);
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
                TranslateNonKeyValue(keyValue[0]);
            }
            else
            {
                TranslateKeyValue(keyValue[0], keyValue[1]);
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
                // 什么都没有的情况下，只记录驱动名称
                // 除了驱动名称外，第一个应该声明驱动的类型
                drivenName = content;
            }
            else
            {


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
						baseTranslator = CreateDrivenTranslator(value);
					}
                    break;
                default:
                    break;
            }
        }
		/// <summary>
		/// 生成相应的驱动解析器
		/// </summary>
		/// <param name="aname">驱动名</param>
		/// <returns>解析器</returns>
		public static BaseDrivenTranslator CreateDrivenTranslator(string aname) {
			BaseDrivenTranslator _translator = null;
			switch (aname) {
				case DrivenConst.BASE_ABILITY:
				default:
					_translator = new BaseDrivenTranslator(drivenName);
					break;
			}
			return _translator;
		}
    }
}
