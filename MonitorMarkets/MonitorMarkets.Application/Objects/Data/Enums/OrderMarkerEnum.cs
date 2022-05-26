namespace MonitorMarkets.Application.Objects.Data.Enums
{
    /// <summary>
    /// Маркер запроса на установку/снятие заявки
    /// </summary>
    public enum OrderMarkerEnum
    {
        Unknown = 0,
        Accepted = 1,
        NotAccepted = 2,
        Cancelled = 3,
        CannotBeCancelled = 4,
    }
}