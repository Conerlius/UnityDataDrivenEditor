
namespace DataDriven
{
    public class AbilityModifierTranslator : BaseDrivenTranslator
    {
        private AbilityModifier modify = null;
        public AbilityModifierTranslator(string aname) : base(aname)
        {
            
        }
        protected override void Init(string aname)
        {
            modify = new AbilityModifier(aname);
        }
        public override object GetObject()
        {
            return modify;
        }
        public override void AddTranslator(BaseDrivenTranslator baseTranslator)
        {
            AbilityEvent _event = baseTranslator.GetObject() as AbilityEvent;
            modify.AddEvent(_event.Name, _event);
        }
    }
}
