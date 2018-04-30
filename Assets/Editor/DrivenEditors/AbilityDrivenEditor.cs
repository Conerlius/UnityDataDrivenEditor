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
        private static string ModifiersIcon = "Toolbar Plus";
        private static string ModifiersName = "修改器";
        /// <summary>
        /// event的编辑器
        /// </summary>
        private List<AbilityEventEditor> eventsEditors = new List<AbilityEventEditor>();
        /// <summary>
        /// modifier的编辑器
        /// </summary>
        private List<AbilityModifierEditor> modifierEditors = new List<AbilityModifierEditor>();
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
                EditorGUI.indentLevel = 1;
                ability.Icon = EditorGUILayout.TextField("技能图标", ability.Icon);

                EditorGUI.indentLevel = 0;
            }
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
                string deleteEventName = string.Empty;
                foreach (var item in ability.events)
                {
                    if (DrawEvents(item.Key, item.Value, _index))
                    {
                        deleteEventName = item.Key;
                    }
                    _index++;
                }
                if (deleteEventName != string.Empty)
                    ability.events.Remove(deleteEventName);
                EditorGUI.indentLevel = 0;
            }
            if (EditorGUILayout.DropdownButton(new GUIContent("添加事件"), FocusType.Keyboard))
            {
                AbilityEventWizard.CreateAbilityEvent(ability);
            }
            #endregion
            #region 修改器
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(EditorGUIUtility.IconContent(ModifiersIcon), GUILayout.Width(20), GUILayout.Height(15)))
            {
                extendModifies = !extendModifies;
                if (extendModifies)
                {
                    ModifiersIcon = "Toolbar Minus";
                }
                else
                {
                    ModifiersIcon = "Toolbar Plus";
                }
            }
            EditorGUILayout.LabelField(ModifiersName);
            EditorGUILayout.EndHorizontal();
            if (extendModifies)
            {
                EditorGUI.indentLevel = 1;
                int _index = 0;
                string deletemodifyName = string.Empty;
                foreach (var item in ability.modifiers)
                {
                    if (DrawModifiers(item.Key, item.Value, _index))
                    {
                        deletemodifyName = item.Key;
                    }
                    _index++;
                }
                if (deletemodifyName != string.Empty)
                    ability.modifiers.Remove(deletemodifyName);
                EditorGUI.indentLevel = 0;
            }
            if (EditorGUILayout.DropdownButton(new GUIContent("添加修改器"), FocusType.Keyboard))
            {
                AbilityModifierWizard.CreateAbilityModifier(ability);
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
        /// <returns>是否删除</returns>
        private bool DrawEvents(string name, AbilityEvent value, int index)
        {
            AbilityEventEditor editor = GetEventEditor(index);
            editor.SetEventAndName(value, name);
            if (editor.Draw())
                return true;
            return false;
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
        /// 展示技能修改器
        /// </summary>
        /// <param name="name">修改器名称</param>
        /// <param name="value">修改器</param>
        /// <param name="index">修改器下标</param>
        /// <returns>是否删除</returns>
        private bool DrawModifiers(string name, AbilityModifier value, int index) {
            AbilityModifierEditor editor = GetModifierEditor(index);
            editor.SetModifierAndName(value, name);
            if (editor.Draw())
                return true;
            return false;
        }
        /// <summary>
        /// 获取修改器展示器
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>修改器展示器</returns>
        private AbilityModifierEditor GetModifierEditor(int index)
        {
            if (modifierEditors.Count <= index)
            {
                modifierEditors.Add(new AbilityModifierEditor());
            }
            return modifierEditors[index];
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
            // 写入属性
            sb.AppendLine("\t\"Driven\"\t\"abilitydriven\"");
            if (abilityDriven.Icon != string.Empty && abilityDriven.Icon.Length > 0)
            {
                sb.AppendLine(string.Format("\t\"Icon\"\t\"{0}\"", abilityDriven.Icon));
            }

            // 写入事件
            WriteAbilityEvent(sb, abilityDriven.events, "\t");
            sb.Append("}");
            return sb.ToString();
        }
        /// <summary>
        /// 写入技能事件
        /// </summary>
        /// <param name="sb">字符流</param>
        /// <param name="events">事件列表</param>
        /// <param name="preTag">tag</param>
        private void WriteAbilityEvent(StringBuilder sb, Dictionary<string, AbilityEvent> events, string preTag)
        {
            foreach (var item in events)
            {
                sb.AppendLine(string.Format("{0}\"{1}\"", preTag, item.Key));
                sb.AppendLine(preTag + "{");
                // 写入事件行为
                AbilityEvent ae = item.Value;
                WriteAbilityAction(sb, ae.Actions, preTag + "\t");

                sb.AppendLine(preTag + "}");
            }

        }
        /// <summary>
        /// 写入行为
        /// </summary>
        /// <param name="sb">字符流</param>
        /// <param name="actions">行为列表</param>
        /// <param name="preTag">tag</param>
        private void WriteAbilityAction(StringBuilder sb, List<AbilityAction> actions, string preTag)
        {
            foreach (var item in actions)
            {
                sb.AppendLine(string.Format("{0}\"{1}\"", preTag, item.Name));
                sb.AppendLine(preTag + "{");
                // 写入事件行为
                item.WriteDetail(sb, preTag + "\t");
                sb.AppendLine(preTag + "}");
            }
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
        private bool extendEvents = false;
        private string EventsIcon = "Toolbar Plus";
        private List<AbilityActionEditor> actionsEditors = new List<AbilityActionEditor>();
        /// <summary>
        /// 展示
        /// </summary>
        /// <returns>是否删除</returns>
        public bool Draw()
        {
            bool isDelete = false;
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
            if (GUILayout.Button(EditorGUIUtility.IconContent("TreeEditor.Trash")))
            {
                isDelete = true;
            }
            EditorGUILayout.EndHorizontal();
            if (extendEvents)
            {
                EditorGUI.indentLevel = 2;
                AbilityAction deleteItem = null;
                for (int _index = 0; _index < _event.Actions.Count; _index++)
                {
                    if (DrawAction(_event.Actions[_index], _index))
                    {
                        deleteItem = _event.Actions[_index];
                    }
                }
                if (deleteItem != null)
                    _event.Actions.Remove(deleteItem);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("", GUILayout.Width(50));
                if (EditorGUILayout.DropdownButton(new GUIContent("添加行为"), FocusType.Keyboard))
                {
                    AbilityActionWizard.CreateAbilityAction(_event);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel = 1;
            }
            return isDelete;
        }
        /// <summary>
        /// 展示技能行为数据
        /// </summary>
        /// <param name="action">行为</param>
        /// <param name="index">行为下标</param>
        /// <returns>是否删除</returns>
        private bool DrawAction(AbilityAction action, int index)
        {
            AbilityActionEditor editor = GetActionEditor(index);
            editor.SetAction(action);
            return editor.Draw();
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
        /// 展开事件的列表
        /// </summary>
        private bool extendaction = false;
        private string actionIcon = "Toolbar Plus";
        /// <summary>
        /// 技能行为
        /// </summary>
        private AbilityAction _action = null;
        public bool Draw()
        {
            bool isDelete = false;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(50));
            if (GUILayout.Button(EditorGUIUtility.IconContent(actionIcon), GUILayout.Width(20), GUILayout.Height(15)))
            {
                extendaction = !extendaction;
                if (extendaction)
                {
                    actionIcon = "Toolbar Minus";
                }
                else
                {
                    actionIcon = "Toolbar Plus";
                }
            }
            EditorGUILayout.LabelField(_action.Name);
            if (GUILayout.Button(EditorGUIUtility.IconContent("TreeEditor.Trash")))
            {
                isDelete = true;
            }
            EditorGUILayout.EndHorizontal();
            if (extendaction)
            {
                EditorGUI.indentLevel = 3;
                _action.Draw();
                EditorGUI.indentLevel = 1;
            }
            return isDelete;
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
