using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DataDriven
{
    /// <summary>
    /// 技能修改器展示器
    /// </summary>
    public class AbilityModifierEditor
    {
        /// <summary>
        /// 修改器
        /// </summary>
        AbilityModifier modifier = null;
        /// <summary>
        /// 是否展开修改器
        /// </summary>
        private bool extendModifier = false;
        private string modifierIcon = "Toolbar Plus";
        private List<AbilityEventEditor> list = new List<AbilityEventEditor>();
        public void SetModifierAndName(AbilityModifier value, string name)
        {
            modifier = value;
        }

        public bool Draw()
        {
            bool isDelete = false;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(20));
            if (GUILayout.Button(EditorGUIUtility.IconContent(modifierIcon), GUILayout.Width(20), GUILayout.Height(15)))
            {
                extendModifier = !extendModifier;
                if (extendModifier)
                {
                    modifierIcon = "Toolbar Minus";
                }
                else
                {
                    modifierIcon = "Toolbar Plus";
                }
            }
            EditorGUILayout.LabelField(modifier.Name);
            if (GUILayout.Button(EditorGUIUtility.IconContent("TreeEditor.Trash")))
            {
                isDelete = true;
            }
            EditorGUILayout.EndHorizontal();
            if (extendModifier) {
                string deleteEventName = string.Empty;
                int index = 0;
                foreach (var item in modifier.events)
                {
                    if (DrawEvents(item.Key, item.Value, index))
                    {
                        deleteEventName = item.Key;
                    }
                    index++;
                }
                if (deleteEventName != string.Empty)
                    modifier.events.Remove(deleteEventName);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("", GUILayout.Width(20));
                if (EditorGUILayout.DropdownButton(new GUIContent("添加事件"), FocusType.Keyboard))
                {
                    AbilityEventWizard.CreateAbilityEvent(modifier);
                }
                EditorGUILayout.EndHorizontal();
            }
            return isDelete;
        }
        /// <summary>
        /// 展示技能事件数据
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="value">事件</param>
        /// <returns>是否删除</returns>
        private bool DrawEvents(string name, AbilityEvent value, int index)
        {
            AbilityEventEditor editor = GetEventEditor(index);
            editor.SetEventAndName(value, name);
            if (editor.Draw(2))
                return true;
            return false;
        }

        private AbilityEventEditor GetEventEditor(int index)
        {
            if (list.Count <= index) {
                list.Add(new AbilityEventEditor());
            }
            
            return list[index];
        }
    }
}
