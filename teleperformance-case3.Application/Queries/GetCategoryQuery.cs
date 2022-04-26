using MediatR;
using teleperformance_case3.Application.Models;

namespace teleperformance_case3.Application.Queries;

public class GetCategoryQuery : IRequest<GetCategoryResponse>
{
    public int Id { get; set; }
}