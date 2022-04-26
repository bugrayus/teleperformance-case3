using MediatR;
using teleperformance_case3.Application.Models;

namespace teleperformance_case3.Application.Queries;

public class GetProductQuery : IRequest<GetProductResponse>
{
    public int Id { get; set; }
}