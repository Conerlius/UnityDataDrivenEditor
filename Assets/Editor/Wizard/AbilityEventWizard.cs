using UnityEditor;

namespace DataDriven
{
    /// <summary>
    /// 添加技能事件引导
    /// </summary>
    public class AbilityEventWizard : ScriptableWizard
    {
        /// <summary>
        /// 技能事件引导单例
        /// </summary>
        static AbilityEventWizard _instance = null;
        /// <summary>
        /// 创建技能事件引导
        /// </summary>
        public static void CreateAbilityEvent()
        {
            if (_instance == null)
            {
                _instance = ScriptableWizard.DisplayWizard<AbilityEventWizard>("创建技能事件", "创建", "取消");
            }
            _instance.Show();
            _instance.Focus();
        }
        /// <summary>  
        /// 点击Add按钮（即Create按钮）调用  
        /// </summary>
        void OnWizardCreate()
        {
            onClose();
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
        }
    }
}