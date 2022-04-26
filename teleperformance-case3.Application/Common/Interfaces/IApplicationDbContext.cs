using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Domain.Entities;

namespace teleperformance_case3.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<Category> Categories { get; }
    public DbSet<Product> Products { get; }
    public DbSet<ShoppingList> ShoppingLists { get; }
    public DbSet<ShoppingListItem> ShoppingListItems { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}