namespace DataDriven
{
    /// <summary>
    /// 驱动编辑器的配置
    /// </summary>
    public static class DDConfig
    {
        /// <summary>
        /// 驱动类型
        /// </summary>
        public enum DrivenType : int
        {
            BaseDriven,    // 基本驱动
            BuildDriven,   // 建筑驱动
            UnitDriven,    // 单位驱动
            CastDriven,    // 施法驱动
            ItemDriven,    // 物品驱动
            AbilityDriven,  // 技能驱动
        }
        /// <summary>
        /// 驱动中文名
        /// </summary>
        public static string[] DrivenTypeName = {
            "基本驱动",
            "建筑驱动",
            "单位驱动",
            "施法驱动",
            "物品驱动",
            "技能驱动",
        };
    }
}
