using System;
using System.IO;
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
        /// 当前驱动的类型
        /// </summary>
        private static DDConfig.DrivenType dtype = DDConfig.DrivenType.BaseDriven;
        /// <summary>
        /// 展示驱动数据
        /// </summary>
        /// <param name="dataDrivenEditor">编辑器</param>
        /// <param name="ability">驱动</param>
        public static void OnGUI(DataDrivenEditor dataDrivenEditor, BaseDriven ability)
        {
            string _type = ability.GetType().ToString();
            _type = _type.Replace("DataDriven.", "");
            dtype = (DDConfig.DrivenType)System.Enum.Parse(typeof(DDConfig.DrivenType), _type);

            EditorGUILayout.BeginVertical();
            if (EditorGUILayout.DropdownButton(new UnityEngine.GUIContent("保存"), UnityEngine.FocusType.Keyboard))
            {
                SaveAbility(dataDrivenEditor, ability);
            }
            // 文件名
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("文件名:");
            EditorGUILayout.LabelField(ability.Name);
            EditorGUILayout.EndHorizontal();
            // 驱动类型
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("驱动类型:");
            int iType = EditorGUILayout.Popup((int)dtype, DDConfig.DrivenTypeName);
            EditorGUILayout.EndHorizontal();

            DrawDrivenEditor(ability, dataDrivenEditor);
            /*EditorGUILayout.BeginHorizontal();
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
*/
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
        /// <summary>
        /// 保存驱动
        /// </summary>
        /// <param name="ability">驱动</param>
        private static void SaveAbility(DataDrivenEditor dataDrivenEditor, BaseDriven ability)
        {
            string content = string.Empty;
            switch (dtype)
            {
                case DDConfig.DrivenType.BuildDriven:
                    content = BuildDrivenEditor.GenerateContent(ability);
                    break;
                case DDConfig.DrivenType.AbilityDriven:
                    content = AbilityDrivenEditor.GenerateContent(ability);
                    break;
                default:
                    content = DDWelcome.GenerateDefaultContent(ability.Name);
                    break;
            }
            if (File.Exists(dataDrivenEditor.FilePath))
            {
                File.Delete(dataDrivenEditor.FilePath);
            }
            FileUtil.CreateTextFile(dataDrivenEditor.FilePath, content);
        }
        /// <summary>
        /// 展示驱动的属性
        /// </summary>
        /// <param name="ability">驱动</param>
        /// <param name="dataDrivenEditor">驱动编辑器</param>
        private static void DrawDrivenEditor(BaseDriven ability, DataDrivenEditor dataDrivenEditor)
        {
            switch (dtype)
            {
                case DDConfig.DrivenType.BuildDriven:
                    {
                        BuildDrivenEditor.OnGUI(ability, dataDrivenEditor);
                    }
                    break;
                case DDConfig.DrivenType.AbilityDriven:
                    {
                        AbilityDrivenEditor.OnGUI(ability, dataDrivenEditor);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
