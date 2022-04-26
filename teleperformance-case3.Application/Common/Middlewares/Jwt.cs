using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CicekSepetiCase.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using teleperformance_case3.Application.Common.Interfaces;

namespace teleperformance_case3.Application.Common.Middlewares;

public class Jwt
{
    private readonly AppSettings _appSettings;
    private readonly RequestDelegate _next;

    public Jwt(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IApplicationDbContext dbContext)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null) await AttachUserToContext(context, dbContext, token);

        await _next(context);
    }

    private async Task AttachUserToContext(HttpContext context, IApplicationDbContext dbContext, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);
            var jwtToken = (JwtSecurityToken) validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "nameid").Value);
            context.Items["User"] = await dbContext.Users.FirstOrDefaultAsync(e => e.Id == userId && e.IsActive);
        }
        catch
        {
            // ignored
        }
    }
}