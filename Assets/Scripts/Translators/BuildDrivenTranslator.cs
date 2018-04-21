
namespace DataDriven
{
    /// <summary>
    /// 建筑的驱动解析器
    /// </summary>
    public class BuildDrivenTranslator : BaseDrivenTranslator
    {
        /// <summary>
        /// 建筑驱动
        /// </summary>
        private BuildDriven ability = null;
        public BuildDrivenTranslator(string aname) : base(aname)
        {

        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="aname">驱动名称</param>
        protected override void Init(string aname)
        {
            if (ability == null)
                ability = new BuildDriven();
            ability.Name = aname;
        }
        public override object GetObject()
        {
            return ability;
        }
        public override void AddKeyValue(string key, string value)
        {
            switch (key) {
                case DrivenPropertyConst.MODELNAME:
                    ability.Model = value;
                    break;
                default:
                    break;
            }
        }
    }
}
