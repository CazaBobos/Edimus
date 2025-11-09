using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Core.Entities;
using Shared.Infrastructure.Extensions;

namespace Shared.Infrastructure.Persistence;
public class DatabaseContext : DbContext
{
    public IConfiguration Configuration { get; }
    public DatabaseContext(IConfiguration configuration) => Configuration = configuration;
    
    #region Tables
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Company> Companies{ get; set; } = null!;
    public DbSet<Establishment> Establishments { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<Ingredient> Ingredients { get; set; } = null!;
    public DbSet<Layout> Layouts { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Request> Requests { get; set; } = null!;
    public DbSet<Table> Tables { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    #endregion

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(true, cancellationToken);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseNpgsql(
            Configuration.GetConnectionString("ConnectionString"),
            options => options.MigrationsAssembly("Edimus.Api")
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureTables();
        modelBuilder.SeedTables();
    }
}
