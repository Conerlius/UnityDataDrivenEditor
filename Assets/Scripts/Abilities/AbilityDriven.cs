
using System.Collections.Generic;

namespace DataDriven
{
    /// <summary>
    /// 技能驱动
    /// </summary>
    public class AbilityDriven : BaseDriven
    {
        /// <summary>
        /// icon
        /// </summary>
        public string Icon = string.Empty;
        /// <summary>
        /// 技能预加载资源
        /// </summary>
        public List<AbilityPrecache> precaches = new List<AbilityPrecache>();
        /// <summary>
        /// 技能事件
        /// </summary>
        public List<AbilityEvent> events = new List<AbilityEvent>();
        /// <summary>
        /// 技能修改器
        /// </summary>
        public List<AbilityModifier> modifiers = new List<AbilityModifier>();
    }
    /// <summary>
    /// 技能预加载资源
    /// </summary>
    public class AbilityPrecache {

    }
    /// <summary>
    /// 技能事件
    /// </summary>
    public class AbilityEvent {

    }
    /// <summary>
    /// 技能修改器
    /// </summary>
    public class AbilityModifier {

    }
}
