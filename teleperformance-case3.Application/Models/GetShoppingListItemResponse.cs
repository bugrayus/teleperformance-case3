namespace teleperformance_case3.Application.Models;

public class GetShoppingListItemResponse
{
    public int Id { get; set; }

    public GetProductResponse Product { get; set; }

    public string Description { get; set; }
    public int Quantity { get; set; }
    public bool IsBought { get; set; } = false;
}