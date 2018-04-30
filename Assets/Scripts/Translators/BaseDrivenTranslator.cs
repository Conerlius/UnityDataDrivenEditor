using System;

namespace DataDriven
{
	/// <summary>
	/// 基本驱动解析器
	/// </summary>
    public class BaseDrivenTranslator
    {
		/// <summary>
		/// 驱动
		/// </summary>
		private BaseDriven ability = null;
		public BaseDrivenTranslator(string aname) {
            Init(aname);
		}
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="aname"></param>
        protected virtual void Init(string aname)
        {
            if (ability == null)
                ability = new BaseDriven();
            ability.Name = aname;
        }
        /// <summary>
        /// 获取当前解析器得到的数据
        /// </summary>
        /// <returns>数据</returns>
        public virtual object GetObject()
        {
            return ability;
        }
        /// <summary>
        /// 添加key-value结构
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public virtual void AddKeyValue(string key, string value)
        {
        }
        /// <summary>
        /// 添加子解析器
        /// </summary>
        /// <param name="baseTranslator">子解析器</param>
        public virtual void AddTranslator(BaseDrivenTranslator baseTranslator)
        {
            
        }
    }
}
