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
        }
        /// <summary>
        /// 当前编辑器的状态
        /// </summary>
        private EditorStatus _status = EditorStatus.Welcome;
        /// <summary>
        /// 构建编辑器窗口
        /// </summary>
        [MenuItem("Tools/DataDriven %1")]
        static void CreateDataDrivenWindow() {
            if (_Instance == null) {
                _Instance = GetWindow<DataDrivenEditor>();
                _Instance.titleContent = new GUIContent(WINDOW_TITLE);
                _Instance.Init();
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
            if (_status == EditorStatus.Welcome) {
                DDWelcome.OnGUI(this);
            }
            
        }

        #region 属性获取
        /// <summary>
        /// 配置路径
        /// </summary>
        public string ConfigPath {
            get;set;
        }
        #endregion
    }
}