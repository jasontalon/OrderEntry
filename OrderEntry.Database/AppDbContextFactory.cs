using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OrderEntry.Database;
using OrderEntry.Infrastructure;

namespace OrderEntry.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        DotEnv.TryLoad();

        var connString = Environment.GetEnvironmentVariable("ConnectionStrings__AppDbContext");

        if (string.IsNullOrEmpty(connString))
            throw new Exception("Environment variable ConnectionStrings__AppDbContext does not exist");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(connString);

        return new AppDbContext(optionsBuilder.Options);
    }
}