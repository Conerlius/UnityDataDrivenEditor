using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DataDriven
{
    public class FileUtil
    {
        /// <summary>
        /// 获取该路径的文件名称
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>文件名称</returns>
        public static string GetFileName(string path)
        {
            string _p = string.Empty;
            path = path.Replace("\\", "/");
            int lastIndex = path.LastIndexOf('.');
            int index = path.LastIndexOf('/');
            _p = path.Substring(index + 1, lastIndex - index - 1);
            return _p;
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="defaultString">默认文本内容</param>
        /// <returns>是否创建成功</returns>
        public static bool CreateTextFile(string path, string defaultString)
        {
            if (File.Exists(path))
            {
                // 文件已经存在
#if UNITY_EDITOR
                if (EditorUtility.DisplayDialog("文件已经存在", "是否需要覆盖掉已经存在的文件", "确定", "取消"))
                {
                    // 删除掉当前已经存在的文件
                    File.Delete(path);
                }
                else
                {
                    return false;
                }
#else
            return false;
#endif
            }
            StreamWriter sw = File.CreateText(path);
            sw.WriteLine(defaultString);
            sw.Flush();
            sw.Close();
            sw = null;
            return true;
        }
    }
}
