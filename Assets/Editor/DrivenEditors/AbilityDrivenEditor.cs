using System;
using System.Text;

namespace DataDriven
{
    /// <summary>
    /// 技能驱动展示器
    /// </summary>
    public class AbilityDrivenEditor
    {
        public static void OnGUI(BaseDriven ability, DataDrivenEditor dataDrivenEditor)
        {
            throw new NotImplementedException();
        }

        public static string GenerateContent(BaseDriven ability)
        {
            BuildDriven buildAbility = ability as BuildDriven;
            StringBuilder sb = new StringBuilder("// This File is auto generated! Don't modify!!!\n");
            sb.AppendFormat("\"{0}\"\n", buildAbility.Name);
            sb.AppendLine("{");
            sb.AppendLine("\t\"Driven\"\t\"abilitydriven\"");

            sb.Append("}");
            return sb.ToString();
        }
    }
}
