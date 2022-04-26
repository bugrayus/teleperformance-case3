using MediatR;

namespace teleperformance_case3.Application.Commands;

public class CreateShoppingListItemCommand : IRequest<bool>
{
    public int ShoppingListId { get; set; }
    public int ProductId { get; set; }
}