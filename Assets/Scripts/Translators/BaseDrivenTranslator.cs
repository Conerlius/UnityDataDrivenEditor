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
		private BaseDriven ability = null;
		public BaseDrivenTranslator(string aname) {
            Init(aname);
		}

        protected virtual void Init(string aname)
        {
            if (ability == null)
                ability = new BaseDriven();
            ability.Name = aname;
        }

        public virtual object GetObject()
        {
            return ability;
        }

        public virtual void AddKeyValue(string key, string value)
        {
        }

        public virtual void AddTranslator(BaseDrivenTranslator baseTranslator)
        {
            
        }
    }
}
