namespace teleperformance_case3.Domain.Entities;

public class ShoppingListItem : BaseEntity
{
    public int ShoppingListId { get; set; }
    public ShoppingList ShoppingList { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public string Description { get; set; }
    public int Quantity { get; set; }
    public bool IsBought { get; set; } = false;
}