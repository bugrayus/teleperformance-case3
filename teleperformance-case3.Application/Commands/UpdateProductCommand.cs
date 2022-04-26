using MediatR;

namespace teleperformance_case3.Application.Commands;

public class UpdateProductCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
}