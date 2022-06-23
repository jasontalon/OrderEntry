using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace OrderEntry.Data;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerAddress> CustomerAddresses { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(p => p.FirstName).HasMaxLength(64);
            entity.Property(p => p.LastName).HasMaxLength(64);
            entity.Property(p => p.Email).HasMaxLength(64);
            entity.Property(p => p.Phone).HasMaxLength(32);
            entity.Property(p => p.CompanyName).HasMaxLength(128);
        });

        modelBuilder.Entity<CustomerAddress>(entity => { entity.HasKey(p => new {p.CustomerId, p.AddressId}); });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(p => p.Address1).HasMaxLength(128);
            entity.Property(p => p.Address2).HasMaxLength(128);
            entity.Property(p => p.City).HasMaxLength(64);
            entity.Property(p => p.State).HasMaxLength(64);
            entity.Property(p => p.Zip).HasMaxLength(32);
            entity.Property(p => p.Country).HasMaxLength(64);
            entity.Property(p => p.Email).HasMaxLength(64);
            entity.Property(p => p.Phone).HasMaxLength(32);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasIndex(p => p.SalesId).IsUnique();
            entity.HasIndex(p => p.TransactionId).IsUnique();            
            
            entity.Property(p => p.Id).HasDefaultValueSql("uuid_generate_v4()");
        });
        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasDefaultValueSql("uuid_generate_v4()");            
        });
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasDefaultValueSql("uuid_generate_v4()");
        });
    }
}