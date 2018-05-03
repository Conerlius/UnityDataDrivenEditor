using UnityEditor;
using UnityEngine;

namespace DataDriven
{
    /// <summary>
    /// 添加技能事件引导
    /// </summary>
    public class AbilityEventWizard : ScriptableWizard
    {
        private static AbilityDriven _ability = null;
        private static AbilityModifier _modifier = null;
        /// <summary>
        /// 技能事件引导单例
        /// </summary>
        private static AbilityEventWizard _instance = null;
        /// <summary>
        /// 事件名称
        /// </summary>
        public DataDrivenConfig.AbilityEventName _eventName = DataDrivenConfig.AbilityEventName.OnSpellStart;
        /// <summary>
        /// 创建技能事件引导
        /// </summary>
        /// <param name="ability">驱动</param>
        public static void CreateAbilityEvent(AbilityDriven ability)
        {
            _ability = ability;
            if (_instance == null)
            {
                _instance = ScriptableWizard.DisplayWizard<AbilityEventWizard>("创建技能事件", "创建", "取消");
            }
            _instance.Show();
            _instance.Focus();
        }
        public static void CreateAbilityEvent(AbilityModifier ability)
        {
            _modifier = ability;
            if (_instance == null)
            {
                _instance = ScriptableWizard.DisplayWizard<AbilityEventWizard>("创建技能事件", "创建", "取消");
            }
            _instance.Show();
            _instance.Focus();
        }
        private void OnGUI()
        {
            _eventName = (DataDrivenConfig.AbilityEventName)EditorGUILayout.EnumPopup(new GUIContent("事件名称"),  _eventName);
            if (EditorGUILayout.DropdownButton(new GUIContent("创建"), FocusType.Keyboard)) {
                OnWizardCreate();
            }
        }
        /// <summary>  
        /// 点击Add按钮（即Create按钮）调用  
        /// </summary>
        void OnWizardCreate()
        {
            string _name = _eventName.ToString();
            if ( _ability!= null && _ability.AddEvent(_name, new AbilityEvent(_name)))
            {
                onClose();
                return;
            }
            if (_modifier != null && _modifier.AddEvent(_name, new AbilityEvent(_name)))
            {
                onClose();
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
        void onClose(){
            this.Close();
            _instance = null;
            _ability = null;
        }
    }
}