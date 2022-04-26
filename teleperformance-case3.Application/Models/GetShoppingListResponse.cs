namespace teleperformance_case3.Application.Models;

public class GetShoppingListResponse
{
    public int Id { get; set; }

    public List<GetShoppingListItemResponse> ShoppingListItems { get; set; }

    public string Name { get; set; }
    public bool IsShopping { get; set; } = false;
}