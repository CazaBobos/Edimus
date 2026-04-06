using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Core.Abstractions;
using Shared.Core.Domain;
using Shared.Core.Entities;
using Shared.Infrastructure.Extensions;

namespace Shared.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public IConfiguration Configuration { get; }
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DatabaseContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        Configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    #region Tables
    public DbSet<AuditLog> AuditLogs { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Consumption> Consumptions { get; set; } = null!;
    public DbSet<Premise> Premises { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<Ingredient> Ingredients { get; set; } = null!;
    public DbSet<Layout> Layouts { get; set; } = null!;
    public DbSet<LayoutCoord> LayoutCoords { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Sector> Sectors { get; set; } = null!;
    public DbSet<SectorCoord> SectorCoords { get; set; } = null!;
    public DbSet<Table> Tables { get; set; } = null!;
    public DbSet<TableCoord> TableCoords { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    #endregion

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var userId = Convert.ToInt32(
                httpContext.User.Claims.FirstOrDefault(c => c.Type == UserClaims.Id)?.Value ?? "0");
            var username = httpContext.User.Claims
                .FirstOrDefault(c => c.Type == UserClaims.Username)?.Value ?? "";

            var entries = ChangeTracker.Entries()
                .Where(e => IsAuditableEntity(e.Metadata.ClrType) &&
                            e.State is EntityState.Added or EntityState.Modified)
                .Select(e =>
                {
                    AuditOperation operation;
                    List<AuditLogChange>? changes = null;

                    if (e.State == EntityState.Added)
                    {
                        operation = AuditOperation.Created;
                    }
                    else
                    {
                        var enabledProp = e.Properties.FirstOrDefault(p => p.Metadata.Name == "Enabled");
                        if (enabledProp?.IsModified == true)
                        {
                            operation = (bool)enabledProp.CurrentValue!
                                ? AuditOperation.Restored
                                : AuditOperation.Removed;
                            changes =
                            [
                                new AuditLogChange(
                                    enabledProp.Metadata.Name,
                                    enabledProp.OriginalValue?.ToString(),
                                    enabledProp.CurrentValue?.ToString())
                            ];
                        }
                        else
                        {
                            operation = AuditOperation.Updated;
                            changes = e.Properties
                                .Where(p => p.IsModified)
                                .Select(p => new AuditLogChange(
                                    p.Metadata.Name,
                                    p.OriginalValue?.ToString(),
                                    p.CurrentValue?.ToString()))
                                .ToList();
                        }
                    }

                    return (Entry: e, EntityType: e.Metadata.ClrType.Name, operation, changes);
                })
                .ToList();

            var result = await base.SaveChangesAsync(true, cancellationToken);

            if (entries.Count > 0)
            {
                var now = DateTime.UtcNow;
                foreach (var (entry, entityType, operation, changes) in entries)
                {
                    var entityId = entry.Properties
                        .FirstOrDefault(p => p.Metadata.IsPrimaryKey())?.CurrentValue?.ToString() ?? "";
                    AuditLogs.Add(new AuditLog(entityType, entityId, operation, userId, username, now, changes));
                }
                await base.SaveChangesAsync(true, cancellationToken);
            }

            return result;
        }

        return await base.SaveChangesAsync(true, cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseNpgsql(
            Configuration.GetConnectionString("ConnectionString")
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureTables();
        modelBuilder.SeedTables();
    }

    private static bool IsAuditableEntity(Type clrType)
    {
        var type = clrType;
        while (type is not null)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Entity<>))
                return true;
            type = type.BaseType;
        }
        return false;
    }
}
