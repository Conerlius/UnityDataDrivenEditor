using UnityEngine;
using UnityEditor;
using System;
using System.Text;

namespace DataDriven
{
    /// <summary>
    /// 建筑驱动
    /// </summary>
    public static class BuildDrivenEditor
    {
        private static string ResPrePath = "Assets/GameAssets/";
        /// <summary>
        /// 模型预览器
        /// </summary>
        private static Editor gameObjectEditor = null;
        /// <summary>
        /// 滚动窗口的展示位置
        /// </summary>
        private static Vector2 scrollPosition = Vector2.zero;
        /// <summary>
        /// 当前的驱动
        /// </summary>
        private static BuildDriven buildAbility = null;
        /// <summary>
        /// 画驱动属性面板
        /// </summary>
        /// <param name="ability">驱动</param>
        public static void OnGUI(BaseDriven ability, DataDrivenEditor dataDrivenEditor)
        {
            float WindowHeiht = dataDrivenEditor.position.height;
            float WindowWidth = dataDrivenEditor.position.width;
            if (buildAbility != ability)
            {
                buildAbility = ability as BuildDriven;
                GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(ResPrePath + buildAbility.Model);
                gameObjectEditor = Editor.CreateEditor(obj);
            }
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();

            float h = EditorGUIUtility.singleLineHeight * 3;

            GUILayout.BeginArea(new Rect(0, h, 500, 500));
            if(gameObjectEditor != null){
                gameObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(500, 500), EditorStyles.whiteLabel);
            }else{
                EditorGUILayout.LabelField("模型加载失败");
            }
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(500, h, WindowWidth - 500, WindowHeiht - h));

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            EditorGUILayout.BeginVertical();
            // 展示所有的属性
            string newModel = EditorGUILayout.TextField("模型名称:", buildAbility.Model);
            if (newModel != buildAbility.Model){
                GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(ResPrePath + newModel);
                buildAbility.Model = newModel;
                gameObjectEditor = Editor.CreateEditor(obj);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();

            GUILayout.EndArea();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }
        /// <summary>
        /// 保存驱动数据
        /// </summary>
        /// <param name="ability"></param>
        public static string GenerateContent(BaseDriven ability)
        {
            BuildDriven buildAbility = ability as BuildDriven;
            StringBuilder sb = new StringBuilder("// This File is auto generated! Don't modify!!!\n");
            sb.AppendFormat("\"{0}\"\n", buildAbility.Name);
            sb.AppendLine("{");
            sb.AppendLine("\t\"Driven\"\t\"builddriven\"");
            sb.AppendLine(string.Format("\t\"Model\"\t\"{0}\"", buildAbility.Model));
            sb.Append("}");
            return sb.ToString();

        }
    }
}