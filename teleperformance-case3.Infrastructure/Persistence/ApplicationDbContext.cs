using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Common.Interfaces;
using teleperformance_case3.Domain.Entities;

namespace teleperformance_case3.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var entities = ChangeTracker.Entries().Where(x =>
            x.Entity is BaseEntity && x.State is EntityState.Added or EntityState.Modified);
        foreach (var entity in entities)
            switch (entity.State)
            {
                case EntityState.Added:
                    ((BaseEntity) entity.Entity).CreatedAt = DateTime.UtcNow;
                    ((BaseEntity) entity.Entity).IsActive = true;
                    break;
                case EntityState.Modified:
                    ((BaseEntity) entity.Entity).UpdatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}