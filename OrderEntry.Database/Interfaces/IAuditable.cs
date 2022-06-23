namespace OrderEntry.Database.Entities;

public interface IAuditable
{
    public DateTimeOffset? CreatedAtUtc { get; set; }
    public DateTimeOffset? UpdatedAtUtc { get; set; }
}