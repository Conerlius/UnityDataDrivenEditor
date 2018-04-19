using System;

namespace DataDriven
{
    /// <summary>
    /// 基本的数据驱动
    /// </summary>
    public class BaseAbility
    {
		/// <summary>
		/// 驱动名称
		/// </summary>
		public string Name = string.Empty;
        /// <summary>
        /// 驱动属性
        /// </summary>
        public Fix64[] Properties;

        public virtual void CopyFrom(BaseAbility ability)
		{
            Name = ability.Name;
		}
	}
}
