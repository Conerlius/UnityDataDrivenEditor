using System;
using System.Reflection;

namespace DataDriven
{
    /// <summary>
    /// driven类型转换,仅在editor使用
    /// </summary>
    public class DataDrivenFactory
    {
        /// <summary>
        /// 解析驱动内容
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>驱动</returns>
        public static BaseDriven ParseConfig(string content)
        {
            return DataDrivenTranslator.Translate(content);
        }

        public static BaseDriven Trans(BaseDriven ability, string className)
        {
            //UnityEngine.Debug.Log(className);
            Assembly asm = Assembly.GetExecutingAssembly();
            if (asm == null)
            {
                return null;
            }
            //2 UnityEditor 内部类
            Type cacheServerType = asm.GetType(className);

            //3 创建实例
            BaseDriven cacheServerPreferencesObj = (BaseDriven)Activator.CreateInstance(cacheServerType);
            //UnityEngine.Debug.Log(cacheServerPreferencesObj.GetType().ToString());
            cacheServerPreferencesObj.CopyFrom(ability);
            return cacheServerPreferencesObj;
        }
        public static AbilityAction CreateAbilityAction(string className) {
            Assembly asm = Assembly.GetExecutingAssembly();
            if (asm == null)
            {
                return null;
            }
            //2 UnityEditor 内部类
            Type cacheServerType = asm.GetType(className+"Action");

            //3 创建实例
            AbilityAction cacheServerPreferencesObj = (AbilityAction)Activator.CreateInstance(cacheServerType);
            return cacheServerPreferencesObj;
        }
    }
}
