﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TechStore.Api.Configurations;
using TechStore.Domain.Interfaces.Services;

namespace TechStore.Domain.Services;

public class TokensService(IOptions<JwtConfiguration> jwtConfig) : ITokensService
{
    public (string token, DateTime expiration) GenerateAccessToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = Encoding.UTF8.GetBytes(jwtConfig.Value.SecretKey);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, "UserId"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "User") ,
            new Claim(JwtRegisteredClaimNames.Iss, jwtConfig.Value.Issuer)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(jwtConfig.Value.ExpirationInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature),
            Audience = jwtConfig.Value.Audience,
            Issuer = jwtConfig.Value.Issuer,
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return (tokenHandler.WriteToken(token), tokenDescriptor.Expires.Value);
    }
}
