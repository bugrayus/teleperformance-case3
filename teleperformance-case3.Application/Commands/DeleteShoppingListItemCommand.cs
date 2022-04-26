using MediatR;

namespace teleperformance_case3.Application.Commands;

public class DeleteShoppingListItemCommand : IRequest<bool>
{
    public int ProductId { get; set; }
    public int ShoppingListId { get; set; }
}