
namespace DataDriven
{
    public class AbilityEventTranslator : BaseDrivenTranslator
    {
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
    }
}
