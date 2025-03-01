using MarketHub.IdentityModule.Api.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MarketHub.IdentityModule.Api.Repositories.Token;

public class TokenUtils(IConfiguration configuration): ITokenUtils
{
    private readonly IConfiguration _configuration = configuration;
    public string GenerateAccessToken(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public  string GenerateRefreshToken(Guid userId)
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        var userIdBytes = userId.ToByteArray();

        var combinedBytes = randomBytes.Concat(userIdBytes).ToArray();
        var hashedBytes = SHA256.HashData(combinedBytes);
        return Convert.ToBase64String(hashedBytes);
    }
}
