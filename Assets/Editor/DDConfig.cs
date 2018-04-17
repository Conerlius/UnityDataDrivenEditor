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
            BaseAbility,    // 基本驱动
            BuildAbility,   // 建筑驱动
            UnitAbility,    // 单位驱动
            CastAbility,    // 施法驱动
            ItemAbility,    // 物品驱动
        }
        /// <summary>
        /// 驱动中文名
        /// </summary>
        public static string[] DrivenTypeName = {
            "基本驱动",
            "建筑驱动",
            "单位驱动",
            "施法驱动",
            "物品驱动"
        };
    }
}
