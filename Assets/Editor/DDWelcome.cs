using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace DataDriven
{
    /// <summary>
    /// 数据驱动编辑器的欢迎界面
    /// </summary>
    public static class DDWelcome
    {
        private static string Fliter = "txt";
        /// <summary>
        /// 渲染窗口
        /// </summary>
        public static void OnGUI(DataDrivenEditor _editor) {
            Vector2 size = _editor.position.size;
            float h = size.y * 0.5f;
            float w = size.x * 0.5f;
            bool isPress = GUI.Button(new Rect(new Vector2(w-50, h-70), new Vector2(100, 60)), new GUIContent("打开"));
            if (isPress) {
                string path = EditorUtility.OpenFilePanel("选择您要打开的文件", _editor.ConfigPath, Fliter);
                if (path != null && path.Length > 0)
                {
                    _editor.FilePath = path;
                    string content = File.ReadAllText(path);
                    _editor.ParseContent(content);
                }
                else
                {
                    _editor.ShowNotification(new GUIContent("用户取消操作"));
                }
            }
            isPress = GUI.Button(new Rect(new Vector2(w -50, h+10), new Vector2(100, 60)), new GUIContent("创建"));
            if (isPress)
            {
                string defaultName = System.DateTime.Now.Ticks.ToString();
                string path = EditorUtility.SaveFilePanelInProject("您要保存的文件", defaultName, Fliter, "message");
                if (path != null && path.Length > 0)
                {
                    string _p = Application.dataPath.Replace("Assets", "") + path;
                    _editor.FilePath = _p;
                    string _name = FileUtil.GetFileName(_p);
                    string _content = GenerateDefaultContent(_name);
                    FileUtil.CreateTextFile(_p, _content);
                    _editor.ParseContent(_content);
                }
                else
                {
                    _editor.ShowNotification(new GUIContent("用户取消操作"));
                }
            }
        }
        /// <summary>
        /// 生成默认文件内容
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>内容</returns>
        public static string GenerateDefaultContent(string fileName) {
            StringBuilder sb = new StringBuilder("// This File is auto generated! Don't modify!!!\n");
            sb.AppendFormat("\"{0}\"\n", fileName);
            sb.AppendLine("{");
            sb.AppendLine(string.Format("\t\"Ability\"\t\"basedriven\""));
            sb.Append("}");
            return sb.ToString();
        }
    }
}