
using System;
using UnityEditor;
using UnityEngine;

namespace DataDriven
{
    /// <summary>
    /// 技能行为引导
    /// </summary>
    public class AbilityActionWizard : ScriptableWizard
    {
        private static AbilityEvent _event = null;
        /// <summary>
        /// 技能事件引导单例
        /// </summary>
        private static AbilityActionWizard _instance = null;
        /// <summary>
        /// 技能行为名称
        /// </summary>
        private DataDrivenConfig.AbilityActionName _actionName = DataDrivenConfig.AbilityActionName.RunScript;
        public static void CreateAbilityAction(AbilityEvent mEvent)
        {
            _event = mEvent;
            if (_instance == null)
            {
                _instance = ScriptableWizard.DisplayWizard<AbilityActionWizard>("创建技能行为", "创建", "取消");
            }
            _instance.Show();
            _instance.Focus();
        }
        private void OnGUI()
        {
            _actionName = (DataDrivenConfig.AbilityActionName)EditorGUILayout.EnumPopup(new GUIContent("行为名称"), _actionName);
            int index = (int)_actionName;
            EditorGUILayout.LabelField(DataDrivenConfig.AbilityActionDes[index]);
            if (EditorGUILayout.DropdownButton(new GUIContent("创建"), FocusType.Keyboard))
            {
                OnWizardCreate();
            }
        }
        /// <summary>  
        /// 点击Add按钮（即Create按钮）调用  
        /// </summary>
        void OnWizardCreate()
        {
            string _name = _actionName.ToString();
            if (_event.AddAction(DataDrivenFactory.CreateAbilityAction("DataDriven."+ _name)))
            {
                onClose();
            }
            else
            {
                errorString = "事件已经存在，不可重复添加";
                isValid = false;
            }
        }
        /// <summary>  
        /// 点击Remove（即other按钮）调用  
        /// </summary>  
        void OnWizardOtherButton()
        {
            onClose();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        void onClose()
        {
            this.Close();
            _instance = null;
            _event = null;
        }
    }
}
