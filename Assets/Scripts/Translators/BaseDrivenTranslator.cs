using System;

namespace DataDriven
{
	/// <summary>
	/// 基本驱动解析器
	/// </summary>
    public class BaseDrivenTranslator
    {
		/// <summary>
		/// 驱动
		/// </summary>
		BaseAbility ability = null;
		public BaseDrivenTranslator(string aname) {
			if (ability == null)
				ability = new BaseAbility();
			ability.Name = aname;
		}

        public object GetObject()
        {
            return ability;
        }
    }
}
