namespace OrderEntry.Database.Entities;

public interface IDescribable
{
    string Name { get; set; }
    string? Description { get; set; }
}