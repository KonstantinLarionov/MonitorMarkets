namespace MonitorMarkets.Databases.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}