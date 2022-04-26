using MediatR;

namespace teleperformance_case3.Application.Commands;

public class CreateShoppingListCommand : IRequest<bool>
{
    public string Name { get; set; }
}