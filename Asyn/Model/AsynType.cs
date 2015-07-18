
namespace ExcelImport.Asyn.Model
{
    /// <summary>
    /// 异步操作类型
    /// </summary>
    public enum AsynType
    {
        AT_NoOrder,                       // 乱序(没有队列操作默认的类型)

        AT_Max,                           // 最大数(新添加都写中间)
    }
}
