﻿using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace teleperformance_case3.Application.Common.Helpers;

public static class PasswordHasher
{
    public static Tuple<string, string> HashPassword(string password, string? saltStr)
    {
        var salt = new byte[128 / 8];
        if (string.IsNullOrEmpty(saltStr))
        {
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
        }
        else
        {
            salt = Convert.FromBase64String(saltStr);
        }

        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA1,
            10000,
            256 / 8));
        return new Tuple<string, string>(Convert.ToBase64String(salt), hashed);
    }
}