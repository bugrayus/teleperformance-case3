using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using teleperformance_case3.Application.Queries;

namespace teleperformance_case3.Controllers;

[Authorize]
[ApiController]
[Route("api/user")]
public class UserController : BaseController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetUserAsync()
    {
        var query = new GetUserQuery();
        var response = await Mediator.Send(query);
        return Success("Users fetched successfully.", response);
    }
}