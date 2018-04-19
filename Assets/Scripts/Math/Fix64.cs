/// <summary>
/// 定点数
/// </summary>
public struct Fix64
{
    private long rawValue;

    #region 重载方法
    public static Fix64 operator +(Fix64 right, Fix64 left)
    {
        return new Fix64() { rawValue = right.rawValue + left.rawValue };
    }
    #endregion
}