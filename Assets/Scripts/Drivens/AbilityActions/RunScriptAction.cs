using System.Text;

namespace DataDriven
{
    /// <summary>
    /// 执行脚本
    /// </summary>
    public class RunScriptAction : AbilityAction
    {
        /// <summary>
        /// 脚本文件
        /// </summary>
        public string FileName = string.Empty;
        /// <summary>
        /// 脚本文件的方法
        /// </summary>
        public string FunctionName = string.Empty;
        public RunScriptAction():base() {
            Name = "RunScript";
        }
#if UNITY_EDITOR
        public override void Draw(int tag)
        {
            UnityEditor.EditorGUILayout.BeginHorizontal();
            UnityEditor.EditorGUILayout.LabelField("", UnityEngine.GUILayout.Width(20 * tag));
            UnityEditor.EditorGUILayout.BeginVertical();
            FileName = UnityEditor.EditorGUILayout.TextField("脚本文件名", FileName);
            FunctionName = UnityEditor.EditorGUILayout.TextField("方法名", FunctionName);
            UnityEditor.EditorGUILayout.EndVertical();
            UnityEditor.EditorGUILayout.EndHorizontal();

        }
        public override void WriteDetail(StringBuilder sb, string pretag)
        {
            sb.AppendLine(string.Format("{0}\"ScriptFile\"\t\"{1}\"", pretag, FileName));
            sb.AppendLine(string.Format("{0}\"Function\"\t\"{1}\"", pretag, FunctionName));
        }
#endif
    }

}