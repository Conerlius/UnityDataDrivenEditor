using System;

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

    }
}
