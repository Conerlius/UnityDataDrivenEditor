using System;
using System.Reflection;

namespace DataDriven
{
    public class DataDrivenFactory
    {
        /// <summary>
        /// 解析驱动内容
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>驱动</returns>
        public static BaseAbility ParseConfig(string content)
        {
            return DataDrivenTranslator.Translate(content);
        }

        public static BaseAbility Trans(BaseAbility ability, string className)
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
            BaseAbility cacheServerPreferencesObj = (BaseAbility)Activator.CreateInstance(cacheServerType);
            //UnityEngine.Debug.Log(cacheServerPreferencesObj.GetType().ToString());
            cacheServerPreferencesObj.CopyFrom(ability);
            return cacheServerPreferencesObj;
        }
    }
}
