
using System;
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
        public Dictionary<string, AbilityEvent> events = new Dictionary<string, AbilityEvent>();
        /// <summary>
        /// 技能修改器
        /// </summary>
        public Dictionary<string, AbilityModifier> modifiers = new Dictionary<string, AbilityModifier>();
        /// <summary>
        /// 添加技能事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="abilityEvent">事件</param>
        /// <returns>是否添加成功</returns>
        public bool AddEvent(string eventName, AbilityEvent abilityEvent)
        {
            if (events.ContainsKey(eventName))
                return false;
            events.Add(eventName, abilityEvent);
            return true;
        }
    }
    /// <summary>
    /// 技能预加载资源
    /// </summary>
    public class AbilityPrecache
    {

    }
    /// <summary>
    /// 技能事件
    /// </summary>
    public class AbilityEvent
    {
        public AbilityEvent(string aname) {
            Name = aname;
        }
        /// <summary>
        /// 事件名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 行为
        /// </summary>
        public Dictionary<string, AbilityAction> Actions = new Dictionary<string, AbilityAction>();
        public bool AddAction(AbilityAction action) {
            if (Actions.ContainsKey(action.Name))
                return false;
            Actions.Add(action.Name, action);
            return true;
        }
    }
    /// <summary>
    /// 技能修改器
    /// </summary>
    public class AbilityModifier
    {

    }
    /// <summary>
    /// 技能行为
    /// </summary>
    public class AbilityAction {
        public AbilityAction(string _name)
        {
            Name = _name;
        }

        /// <summary>
        /// 行为名称
        /// </summary>
        public string Name { get; private set; }
    }
}
