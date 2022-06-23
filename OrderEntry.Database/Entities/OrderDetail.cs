namespace OrderEntry.Data;

public class OrderDetail : IAuditable
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public int LineItemSequence { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public string? ProductCategory { get; set; }
    public Guid? ProductId { get; set; }
    public string UnitOfMeasurement { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public int Quantity { get; set; }

    public decimal ExtendedPrice { get; set; }

    public virtual Order Order { get; set; }
    public virtual Product? Product { get; set; }
    public DateTimeOffset? CreatedAtUtc { get; set; }
    public DateTimeOffset? UpdatedAtUtc { get; set; }
}