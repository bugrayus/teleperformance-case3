namespace teleperformance_case3.Domain.Entities;

public class ShoppingList : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public List<ShoppingListItem> ShoppingListItems { get; set; }

    public string Name { get; set; }
    public bool IsShopping { get; set; } = false;
}