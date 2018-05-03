using System.Collections.Generic;
namespace DataDriven
{
    public class AbilityModifiersTranslator : BaseDrivenTranslator
    {
        private Dictionary<string, AbilityModifier> dict = new Dictionary<string, AbilityModifier>();
        public AbilityModifiersTranslator(string aname) : base(aname)
        {
        }
        protected override void Init(string aname)
        {
        }
        public override object GetObject()
        {
            return dict;
        }
        public override void AddTranslator(BaseDrivenTranslator baseTranslator)
        {
            AbilityModifier modify = baseTranslator.GetObject() as AbilityModifier;
            dict.Add(modify.Name, modify);
        }
    }
}
