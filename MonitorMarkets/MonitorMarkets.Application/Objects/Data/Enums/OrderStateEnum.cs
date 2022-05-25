namespace MonitorMarkets.Application.Objects.Data.Enums
{
    public enum OrderStateEnum
    {
        None = 0,
        Opened = 1,
        Partial = 2,
        Filled = 3,
        Removed = 4,
        OpenFailed = 5,
        CancelFailed = 6,
    }
}