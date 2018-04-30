using UnityEditor;
using UnityEngine;

namespace DataDriven
{
    public class AbilityModifierWizard : ScriptableWizard
    {
        private static AbilityDriven _ability = null;
        /// <summary>
        /// 技能修改器引导单例
        /// </summary>
        private static AbilityModifierWizard _instance = null;
        /// <summary>
        /// 技能修改器名称
        /// </summary>
        private string ModifierName = string.Empty;
        public static void CreateAbilityModifier(AbilityDriven ability)
        {
            _ability = ability;
            if (_instance == null)
            {
                _instance = ScriptableWizard.DisplayWizard<AbilityModifierWizard>("创建技能修改器", "创建", "取消");
            }
            _instance.Show();
            _instance.Focus();
        }
        private void OnGUI()
        {
            ModifierName = EditorGUILayout.TextField("技能修改器名称", ModifierName);
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
            if (_ability.AddModifier(ModifierName, new AbilityModifier(ModifierName)))
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
            _ability = null;
        }
    }
}