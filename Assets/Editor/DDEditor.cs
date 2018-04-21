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
        /// 当前驱动的类型
        /// </summary>
        private static DDConfig.DrivenType dtype = DDConfig.DrivenType.BaseDriven;
        /// <summary>
        /// 基本驱动展示器
        /// </summary>
        private static BaseDrivenEditor _editor = null;
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

            _editor = SwitchDrawDrivenEditor();

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
            if (_editor != null)
                _editor.OnGUI(ability, dataDrivenEditor);
            
            EditorGUILayout.EndVertical();

            if (iType != (int)dtype)
            {
                // 转换类型，可能会丢失数据
                if (EditorUtility.DisplayDialog("转换类型", "转换类型可能会引起数据丢失!!确定需要转换?", "确定", "取消"))
                {
                    // 关闭展示
                    if (_editor != null)
                        _editor.CloseDrawDriven();
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
            if (_editor != null)
                content = _editor.GenerateContent(ability);
            if (File.Exists(dataDrivenEditor.FilePath))
            {
                File.Delete(dataDrivenEditor.FilePath);
            }
            FileUtil.CreateTextFile(dataDrivenEditor.FilePath, content);
        }
        private static void CloseDrawDriven() {

        }
        /// <summary>
        /// 展示驱动
        /// </summary>
        private static BaseDrivenEditor SwitchDrawDrivenEditor()
        {
            BaseDrivenEditor _ed = null;
            switch (dtype)
            {
                case DDConfig.DrivenType.BuildDriven:
                    {
                        _ed = BuildDrivenEditor.Instance;
                    }
                    break;
                case DDConfig.DrivenType.AbilityDriven:
                    {
                        _ed = AbilityDrivenEditor.Instance;
                    }
                    break;
                default:
                    break;
            }
            return _ed;
        }
    }
}
