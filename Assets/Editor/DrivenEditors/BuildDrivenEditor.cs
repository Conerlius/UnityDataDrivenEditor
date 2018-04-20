using UnityEngine;
using UnityEditor;
using System;

namespace DataDriven
{
    /// <summary>
    /// 建筑驱动
    /// </summary>
    public static class BuildDrivenEditor
    {
        private static Editor gameObjectEditor = null;
        /// <summary>
        /// 画驱动属性面板
        /// </summary>
        /// <param name="ability">驱动</param>
        public static void OnGUI(BaseDriven ability)
        {
            BuildAbility buildAbility = ability as BuildAbility;
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();

            float h = EditorGUIUtility.singleLineHeight * 3;

            GUILayout.BeginArea(new Rect(0, h, 500, 500));
            if(gameObjectEditor != null){
                gameObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(500, 500), EditorStyles.whiteLabel);
            }else{
                EditorGUILayout.LabelField("shibai");
            }
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(510, h, 600, 700));

            EditorGUILayout.BeginVertical();
            string newModel = EditorGUILayout.TextField("模型名称:", buildAbility.Model);
            if (newModel != buildAbility.Model){
                GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(newModel);
                buildAbility.Model = newModel;
                gameObjectEditor = Editor.CreateEditor(obj);
            }
            EditorGUILayout.EndVertical();
            GUILayout.EndArea();
            EditorGUILayout.EndHorizontal();
            /*gameObject = (GameObject)EditorGUILayout.ObjectField(gameObject, typeof(GameObject), true);
            if (gameObject != null)
            {
                if (gameObjectEditor == null)
                {
                    gameObjectEditor = Editor.CreateEditor(gameObject);
                }
                gameObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(500, 500), EditorStyles.whiteLabel);
            }*/
            EditorGUILayout.EndVertical();
        }
    }
}