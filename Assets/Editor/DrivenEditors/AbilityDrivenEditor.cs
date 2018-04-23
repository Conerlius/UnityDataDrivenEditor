using System;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace DataDriven
{
    /// <summary>
    /// 技能驱动展示器
    /// </summary>
    public class AbilityDrivenEditor: BaseDrivenEditor
    {
        static AbilityDrivenEditor _instance = null;
        public static AbilityDrivenEditor Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AbilityDrivenEditor();
                }
                return _instance;
            }
        }
        /// <summary>
        /// 展开属性的列表
        /// </summary>
        private static bool extendProperties = false;
        private static string PropertiesIcon = "Toolbar Plus";
        private static string PropertiesName = "驱动属性";
        /// <summary>
        /// 展开事件的列表
        /// </summary>
        private static bool extendEvents = false;
        /// <summary>
        /// 展开modify的列表
        /// </summary>
        private static bool extendModifies = false;
        public void OnGUI(BaseDriven ability, DataDrivenEditor dataDrivenEditor)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(EditorGUIUtility.IconContent(PropertiesIcon),GUILayout.Width(20), GUILayout.Height(15))){
                extendProperties = !extendProperties;
                if (extendProperties){
                    PropertiesIcon = "Toolbar Minus";
                }else{
                    PropertiesIcon = "Toolbar Plus";
                }
            }
            EditorGUILayout.LabelField(PropertiesName);
            EditorGUILayout.EndHorizontal();
            if (extendProperties){
                
            }
            EditorGUILayout.EndVertical();
        }

        public string GenerateContent(BaseDriven driven)
        {
            AbilityDriven abilityDriven = driven as AbilityDriven;
            StringBuilder sb = new StringBuilder("// This File is auto generated! Don't modify!!!\n");
            sb.AppendFormat("\"{0}\"\n", abilityDriven.Name);
            sb.AppendLine("{");
            sb.AppendLine("\t\"Driven\"\t\"abilitydriven\"");

            sb.Append("}");
            return sb.ToString();
        }

        public void CloseDrawDriven()
        {
            
        }
    }
}
