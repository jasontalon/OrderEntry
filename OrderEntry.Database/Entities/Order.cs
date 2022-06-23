namespace OrderEntry.Data;

public class Order : IAuditable
{
    public Guid Id { get; set; }
    public string SalesId { get; set; }
    public string TransactionId { get; set; }
    
    public DateOnly OrderDate { get; set; }
    public string OrderType { get; set; }
    public Guid? CustomerId { get; set; }
    public string ContactName { get; set; }
    public string CompanyName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public Guid? ShipMethodId { get; set; }

    public decimal ShippingAmount { get; set; }

    public decimal SubTotalAmount { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal GrandTotalAmount { get; set; }

    public Guid? BillToAddressId { get; set; }
    public Guid? ShipToAddressId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Address? BillToAddress { get; set; }
    public virtual Address? ShipToAddress { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    public DateTimeOffset? CreatedAtUtc { get; set; }
    public DateTimeOffset? UpdatedAtUtc { get; set; }
    public virtual ShipMethod? ShipMethod { get; set; }
}