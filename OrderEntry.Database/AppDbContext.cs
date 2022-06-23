using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderEntry.Database.Entities;

namespace OrderEntry.Database;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerAddress> CustomerAddresses { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ShipMethod> ShipMethods { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Customer>(builder =>
        {
            builder.ConfigureKey();
            builder.Property(p => p.FirstName).HasMaxLength(64);
            builder.Property(p => p.LastName).HasMaxLength(64);
            builder.Property(p => p.Email).HasMaxLength(64);
            builder.Property(p => p.Phone).HasMaxLength(32);
            builder.Property(p => p.CompanyName).HasMaxLength(128);
        });

        modelBuilder.Entity<CustomerAddress>(builder => builder.HasKey(p => new {p.CustomerId, p.AddressId}));

        modelBuilder.Entity<Address>(builder =>
        {
            builder.ConfigureKey();

            builder.Property(p => p.Address1).HasMaxLength(128);
            builder.Property(p => p.Address2).HasMaxLength(128);
            builder.Property(p => p.City).HasMaxLength(64);
            builder.Property(p => p.State).HasMaxLength(64);
            builder.Property(p => p.Zip).HasMaxLength(32);
            builder.Property(p => p.Country).HasMaxLength(64);
            builder.Property(p => p.Email).HasMaxLength(64);
            builder.Property(p => p.Phone).HasMaxLength(32);
        });

        modelBuilder.Entity<Order>(builder =>
        {
            builder.ConfigureKey();

            builder.HasIndex(p => p.SalesId).IsUnique();
            builder.HasIndex(p => p.TransactionId).IsUnique();
        });
        modelBuilder.Entity<OrderDetail>(builder => builder.ConfigureKey());
        modelBuilder.Entity<Product>(builder => builder
            .ConfigureKey()
            .ConfigureDescription());

        modelBuilder.Entity<ShipMethod>(builder => builder
            .ConfigureKey()
            .ConfigureDescription());
    }
}

public static class Helper
{
    public static EntityTypeBuilder<TEntity> ConfigureKey<TEntity>(
        this EntityTypeBuilder<TEntity> builder
    )
        where TEntity : class, IKey
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasDefaultValueSql("uuid_generate_v4()");

        return builder;
    }

    public static EntityTypeBuilder<TEntity> ConfigureDescription<TEntity>(
        this EntityTypeBuilder<TEntity> builder
    )
        where TEntity : class, IDescribable
    {
        builder.HasIndex(p => p.Name).IsUnique();
        builder.Property(p => p.Name).HasMaxLength(128);
        builder.Property(p => p.Description).HasMaxLength(512);

        return builder;
    }
}