
namespace DataDriven
{
    /// <summary>
    /// 技能驱动解析器
    /// </summary>
    public class AbilityDrivenTranslator : BaseDrivenTranslator
    {
        private AbilityDriven ability = null;
        public AbilityDrivenTranslator(string aname) : base(aname)
        {
        }
        protected override void Init(string aname)
        {
            if (ability == null) {
                ability = new AbilityDriven();
            }
            ability.Name = aname;
        }
        public override object GetObject()
        {
            return ability;
        }

    }
}
