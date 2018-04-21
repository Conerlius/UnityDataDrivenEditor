namespace DataDriven
{
    /// <summary>
    /// 基本驱动展示其器
    /// </summary>
    public interface BaseDrivenEditor
    {
        string GenerateContent(BaseDriven ability);
        void OnGUI(BaseDriven ability, DataDrivenEditor dataDrivenEditor);
        void CloseDrawDriven();
    }
}
