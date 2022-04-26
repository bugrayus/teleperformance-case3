using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Models;

namespace teleperformance_case3.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController : BaseController
{
    private readonly IMapper _mapper;

    public UserController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var command = _mapper.Map<LoginCommand>(request);
        var response = await Mediator.Send(command);
        return Success("User logged in successfully.", response);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var response = await Mediator.Send(command);
        return Success("User registered successfully.", response);
    }
}