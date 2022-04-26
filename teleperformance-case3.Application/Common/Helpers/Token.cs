﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CicekSepetiCase.Core.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using teleperformance_case3.Domain.Entities;

namespace teleperformance_case3.Application.Common.Helpers;

public class Token
{
    private readonly AppSettings _appSettings;

    public Token(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                //new Claim(ClaimTypes.Name, user.FirstName),
                //new Claim(ClaimTypes.Surname, user.LastName),
                //new Claim(ClaimTypes.Email, user.Email),
                //new Claim(ClaimTypes.MobilePhone, user.GsmNumber),
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha384Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenStr = tokenHandler.WriteToken(token);
        return tokenStr;
    }
}