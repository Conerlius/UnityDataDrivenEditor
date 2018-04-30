using System;

namespace DataDriven
{
    /// <summary>
    /// 基本的数据驱动
    /// </summary>
    public class BaseDriven
    {
		/// <summary>
		/// 驱动名称
		/// </summary>
		public string Name = string.Empty;

        public virtual void CopyFrom(BaseDriven ability)
		{
            Name = ability.Name;
		}
	}
}
