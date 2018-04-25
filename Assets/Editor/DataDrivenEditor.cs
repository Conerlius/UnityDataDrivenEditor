using System;
using UnityEditor;
using UnityEngine;

namespace DataDriven
{
    /// <summary>
    /// 数据驱动编辑工具
    /// </summary>
    public class DataDrivenEditor : EditorWindow
    {
        /// <summary>
        /// 配置路径
        /// </summary>
        public static string CONFIG_PATH = "";
        /// <summary>
        /// 窗口标题
        /// </summary>
        private static string WINDOW_TITLE = "DataDriven编辑器";
        /// <summary>
        /// 窗口单例
        /// </summary>
        private static DataDrivenEditor _Instance = null;
        /// <summary>
        /// 编辑器状态枚举
        /// </summary>
        private enum EditorStatus {
            /// <summary>
            /// 欢迎界面
            /// </summary>
            Welcome,
            /// <summary>
            /// 编辑模式
            /// </summary>
            Editor,
        }
        /// <summary>
        /// 当前编辑器的状态
        /// </summary>
        private EditorStatus _status = EditorStatus.Welcome;
        /// <summary>
        /// 基本的数据驱动
        /// </summary>
        private BaseDriven ability = null;
        /// <summary>
        /// 构建编辑器窗口
        /// </summary>
        [MenuItem("Tools/DataDriven #1")]
        static void CreateDataDrivenWindow() {
            if (_Instance == null) {
                _Instance = GetWindow<DataDrivenEditor>();
                _Instance.titleContent = new GUIContent(WINDOW_TITLE);
                _Instance.Init();
                _Instance.minSize = new Vector2(1000, 600);
            }
            // 展示窗口
            _Instance.Show();
            // 聚焦展示
            _Instance.Focus();
        }
        /// <summary>
        /// 初始化编辑器
        /// </summary>
        private void Init() {
            ConfigPath = Application.dataPath + CONFIG_PATH;
        }
        /// <summary>
        /// gui渲染
        /// </summary>
        private void OnGUI()
        {
            if (_status == EditorStatus.Welcome)
            {
                DDWelcome.OnGUI(this);
            }
            else if (_status == EditorStatus.Editor){
                if (ability == null){
                    _status = EditorStatus.Welcome;
                    return;
                }
                DDEditor.OnGUI(this, ability);
            }
            
        }
        /// <summary>
        /// 解析驱动内容
        /// </summary>
        /// <param name="content">内容</param>
        public void ParseContent(string content)
        {
            ability = DataDrivenFactory.ParseConfig(content);
            if (ability == null) {
                this.ShowNotification(new GUIContent("解析驱动失败!!!"));
                return;
            }
            _status = EditorStatus.Editor;
        }
        /// <summary>
        /// 切换驱动类型
        /// </summary>
        /// <param name="iType">目标类型</param>
        public void ChangeDrivenTypeTo(DataDrivenConfig.DrivenType iType)
        {
            ability = DataDrivenFactory.Trans(ability, "DataDriven." + System.Enum.GetName(typeof(DataDrivenConfig.DrivenType), iType));
        }
        /// <summary>
        /// 保存现在对驱动的修改
        /// </summary>
        public void SaveAbility(){
            
        }
        #region 属性获取
        /// <summary>
        /// 配置路径
        /// </summary>
        public string ConfigPath {
            get;set;
        }
        /// <summary>
        /// 打开的文件路径
        /// </summary>
        public string FilePath { get; set; }
        #endregion
    }
}