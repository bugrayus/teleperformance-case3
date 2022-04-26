using MediatR;

namespace teleperformance_case3.Application.Commands;

public class RegisterCommand : IRequest<bool>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }
}