namespace MonitorMarkets.Application.Objects.Data.Enums
{
    public enum TimeInForceEnum
    { 
        Unrecognized,
        None, 
        GoodTillCancel, 
        ImmediateOrCancel,
        FillOrKill, 
        PostOnly,
    }
}