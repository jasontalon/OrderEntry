namespace OrderEntry.Database.Entities;

public class CustomerAddress
{
    public Guid CustomerId { get; set; }
    public Guid AddressId { get; set; }

    public bool? Primary { get; set; }

    public virtual Customer Customer { get; set; }
    public virtual Address Address { get; set; }
}