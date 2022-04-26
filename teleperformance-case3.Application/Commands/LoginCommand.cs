using MediatR;
using teleperformance_case3.Application.Models;

namespace teleperformance_case3.Application.Commands;

public class LoginCommand : IRequest<LoginResponse>
{
    public string Mail { get; set; }
    public string Password { get; set; }
}