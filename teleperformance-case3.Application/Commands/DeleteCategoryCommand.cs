using MediatR;

namespace teleperformance_case3.Application.Commands;

public class DeleteCategoryCommand : IRequest<bool>
{
    public int Id { get; set; }
}