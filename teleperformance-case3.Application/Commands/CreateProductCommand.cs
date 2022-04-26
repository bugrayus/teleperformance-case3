using MediatR;

namespace teleperformance_case3.Application.Commands;

public class CreateProductCommand : IRequest<bool>
{
    public string Name { get; set; }
    public int CategoryId { get; set; }
}