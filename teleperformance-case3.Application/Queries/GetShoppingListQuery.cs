using MediatR;
using teleperformance_case3.Application.Models;

namespace teleperformance_case3.Application.Queries;

public class GetShoppingListQuery : IRequest<GetShoppingListResponse>
{
    public int Id { get; set; }
}