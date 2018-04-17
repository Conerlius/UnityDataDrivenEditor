using System;
using UnityEditor;

namespace DataDriven
{
    /// <summary>
    /// 驱动编辑界面
    /// </summary>
    public static class DDEditor
    {
        /// <summary>
        /// 展示驱动数据
        /// </summary>
        /// <param name="dataDrivenEditor">编辑器</param>
        /// <param name="ability">驱动</param>
        public static void OnGUI(DataDrivenEditor dataDrivenEditor, BaseAbility ability)
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
            if (iType != (int)dtype) {
                // 转换类型，可能会丢失数据
                if (EditorUtility.DisplayDialog("转换类型", "转换类型可能会引起数据丢失!!确定需要转换?", "确定", "取消"))
                {
                    dataDrivenEditor.ChangeDrivenTypeTo((DDConfig.DrivenType)iType);
                }
                else {
                    dataDrivenEditor.ShowNotification(new UnityEngine.GUIContent("取消转换"));
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }
}
