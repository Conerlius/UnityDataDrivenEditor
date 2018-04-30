namespace DataDriven
{
    /// <summary>
    /// 执行脚本行为的解析器
    /// </summary>
    public class RunScriptTranslator : AbilityActionTranslator
    {
        /// <summary>
        /// 行为
        /// </summary>
        private RunScriptAction action = null;
        public RunScriptTranslator(string aname) : base(aname)
        {
            action = new RunScriptAction();
        }
        public override void AddKeyValue(string key, string value)
        {
            switch (key) {
                case "ScriptFile":
                    action.FileName = value;
                    break;
                case "Function":
                    action.FunctionName = value;
                    break;
                default:
                    break;
            }
        }
        public override object GetObject()
        {
            return action;
        }
    }
}
