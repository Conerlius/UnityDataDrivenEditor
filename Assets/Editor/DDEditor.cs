using System;
using UnityEditor;
using UnityEngine;

namespace DataDriven
{
    /// <summary>
    /// 驱动编辑界面
    /// </summary>
    public static class DDEditor
    {
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
        /// <summary>
        /// 展示驱动数据
        /// </summary>
        /// <param name="dataDrivenEditor">编辑器</param>
        /// <param name="ability">驱动</param>
        public static void OnGUI(DataDrivenEditor dataDrivenEditor, BaseDriven ability)
        {
            EditorGUILayout.BeginVertical();
            // 文件名
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("文件名:");
            EditorGUILayout.LabelField(ability.Name);
            EditorGUILayout.EndHorizontal();
            // 驱动类型
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("驱动类型:");
            string _type = ability.GetType().ToString();
            _type = _type.Replace("DataDriven.", "");
            DDConfig.DrivenType dtype = (DDConfig.DrivenType)System.Enum.Parse(typeof(DDConfig.DrivenType), _type);
            int iType = EditorGUILayout.Popup((int)dtype, DDConfig.DrivenTypeName);
            EditorGUILayout.EndHorizontal();

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

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (EditorGUILayout.DropdownButton(new UnityEngine.GUIContent("保存"), UnityEngine.FocusType.Keyboard)){
                dataDrivenEditor.SaveAbility();
            }
            EditorGUILayout.EndVertical();

            if (iType != (int)dtype)
            {
                // 转换类型，可能会丢失数据
                if (EditorUtility.DisplayDialog("转换类型", "转换类型可能会引起数据丢失!!确定需要转换?", "确定", "取消"))
                {
                    dataDrivenEditor.ChangeDrivenTypeTo((DDConfig.DrivenType)iType);
                }
                else
                {
                    dataDrivenEditor.ShowNotification(new UnityEngine.GUIContent("取消转换"));
                }
            }
        }
    }
}
