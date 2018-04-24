using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace DataDriven
{
    /// <summary>
    /// 技能驱动展示器
    /// </summary>
    public class AbilityDrivenEditor : BaseDrivenEditor
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
        private static string EventsIcon = "Toolbar Plus";
        private static string EventsName = "技能事件";
        /// <summary>
        /// 展开modify的列表
        /// </summary>
        private static bool extendModifies = false;
        /// <summary>
        /// event的编辑器
        /// </summary>
        private List<AbilityEventEditor> eventsEditors = new List<AbilityEventEditor>();
        public void OnGUI(BaseDriven driven, DataDrivenEditor dataDrivenEditor)
        {
            AbilityDriven ability = driven as AbilityDriven;
            EditorGUILayout.BeginVertical();
            #region 属性
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(EditorGUIUtility.IconContent(PropertiesIcon), GUILayout.Width(20), GUILayout.Height(15)))
            {
                extendProperties = !extendProperties;
                if (extendProperties)
                {
                    PropertiesIcon = "Toolbar Minus";
                }
                else
                {
                    PropertiesIcon = "Toolbar Plus";
                }
            }
            EditorGUILayout.LabelField(PropertiesName);
            EditorGUILayout.EndHorizontal();

            if (extendProperties)
            {
                EditorGUI.indentLevel  = 1;
                ability.Icon = EditorGUILayout.TextField("技能图标", ability.Icon);
            }
            EditorGUI.indentLevel = 0;
            #endregion
            #region 事件
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(EditorGUIUtility.IconContent(EventsIcon), GUILayout.Width(20), GUILayout.Height(15)))
            {
                extendEvents = !extendEvents;
                if (extendEvents)
                {
                    EventsIcon = "Toolbar Minus";
                }
                else
                {
                    EventsIcon = "Toolbar Plus";
                }
            }
            EditorGUILayout.LabelField(EventsName);
            EditorGUILayout.EndHorizontal();
            if (extendEvents)
            {
                EditorGUI.indentLevel = 1;
                int _index = 0;
                foreach (var item in ability.events)
                {
                    DrawEvents(item.Key, item.Value, _index);
                    _index++;
                }
            }
            EditorGUI.indentLevel = 0;
            if (EditorGUILayout.DropdownButton(new GUIContent("添加事件"), FocusType.Keyboard))
            {
                AbilityEventWizard.CreateAbilityEvent(ability);
            }
            #endregion
            EditorGUILayout.EndVertical();
        }
        /// <summary>
        /// 展示技能事件数据
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="value">事件</param>
        /// <param name="index">事件下标</param>
        private void DrawEvents(string name, AbilityEvent value, int index)
        {
            AbilityEventEditor editor = GetEventEditor(index);
            editor.SetEventAndName(value, name);
            editor.Draw();
        }
        /// <summary>
        /// 获取事件展示器
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>事件展示器</returns>
        private AbilityEventEditor GetEventEditor(int index)
        {
            if (eventsEditors.Count <= index)
            {
                eventsEditors.Add(new AbilityEventEditor());
            }
            return eventsEditors[index];
        }
        /// <summary>
        /// 生成驱动文本
        /// </summary>
        /// <param name="driven">驱动</param>
        /// <returns>驱动内容</returns>
        public string GenerateContent(BaseDriven driven)
        {
            AbilityDriven abilityDriven = driven as AbilityDriven;
            StringBuilder sb = new StringBuilder("// This File is auto generated! Don't modify!!!\n");
            sb.AppendFormat("\"{0}\"\n", abilityDriven.Name);
            sb.AppendLine("{");
            sb.AppendLine("\t\"Driven\"\t\"abilitydriven\"");
            if (abilityDriven.Icon != string.Empty && abilityDriven.Icon.Length > 0)
            {
                sb.AppendLine(string.Format("\t\"Icon\"\t\"{0}\"", abilityDriven.Icon));
            }

            sb.Append("}");
            return sb.ToString();
        }

        public void CloseDrawDriven()
        {

        }
    }
    /// <summary>
    /// 技能事件展示器
    /// </summary>
    public class AbilityEventEditor
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        private string Name = String.Empty;
        /// <summary>
        /// 事件
        /// </summary>
        private AbilityEvent _event = null;
        /// <summary>
        /// 展开事件的列表
        /// </summary>
        private static bool extendEvents = false;
        private static string EventsIcon = "Toolbar Plus";
        private static List<AbilityActionEditor> actionsEditors = new List<AbilityActionEditor>();
        /// <summary>
        /// 展示
        /// </summary>
        public void Draw()
        {
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(20));
            if (GUILayout.Button(EditorGUIUtility.IconContent(EventsIcon), GUILayout.Width(20), GUILayout.Height(15)))
            {
                extendEvents = !extendEvents;
                if (extendEvents)
                {
                    EventsIcon = "Toolbar Minus";
                }
                else
                {
                    EventsIcon = "Toolbar Plus";
                }
            }
            EditorGUILayout.LabelField(Name);
            EditorGUILayout.EndHorizontal();
            if (extendEvents){
                EditorGUI.indentLevel = 2;
                int _index = 0;
                foreach (var item in _event.Actions) {
                    DrawAction(item, _index);
                    _index++;
                }
                EditorGUI.indentLevel = 1;
            }
            
        }
        /// <summary>
        /// 展示技能行为数据
        /// </summary>
        /// <param name="action">行为</param>
        /// <param name="index">行为下标</param>
        private void DrawAction(AbilityAction action, int index) {
            AbilityActionEditor editor = GetActionEditor(index);
            editor.SetAction(action);
            editor.Draw();
        }
        /// <summary>
        /// 获取行为展示器
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>行为展示器</returns>
        private AbilityActionEditor GetActionEditor(int index)
        {
            if (actionsEditors.Count <= index)
            {
                actionsEditors.Add(new AbilityActionEditor());
            }
            return actionsEditors[index];
        }
        /// <summary>
        /// 绑定事件的名称和事件
        /// </summary>
        /// <param name="value">事件</param>
        /// <param name="name">事件名称</param>
        public void SetEventAndName(AbilityEvent value, string name)
        {
            _event = value;
            Name = name;
        }
    }
    /// <summary>
    /// 技能行为展示器
    /// </summary>
    public class AbilityActionEditor
    {
        /// <summary>
        /// 技能行为
        /// </summary>
        private AbilityAction _action = null;
        public void Draw()
        {
            
        }
        /// <summary>
        /// 绑定技能行为
        /// </summary>
        /// <param name="action">行为</param>
        public void SetAction(AbilityAction action)
        {
            _action = action;
        }
    }
}
