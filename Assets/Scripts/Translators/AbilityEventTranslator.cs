
namespace DataDriven
{
    /// <summary>
    /// 技能事件解析器
    /// </summary>
    public class AbilityEventTranslator : BaseDrivenTranslator
    {
        /// <summary>
        /// 技能事件
        /// </summary>
        private AbilityEvent _event = null;
        public AbilityEventTranslator(string aname) : base(aname)
        {
            _event = new AbilityEvent(aname);
        }
        protected override void Init(string aname)
        {
        }
        public override object GetObject()
        {
            return _event;
        }
        public override void AddKeyValue(string key, string value)
        {
            
        }
        public override void AddTranslator(BaseDrivenTranslator baseTranslator)
        {
            AbilityAction action = (AbilityAction)baseTranslator.GetObject();
            _event.AddAction(action);
        }
    }
}
