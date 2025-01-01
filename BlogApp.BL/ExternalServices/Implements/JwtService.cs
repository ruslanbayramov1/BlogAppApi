using BlogApp.BL.DTOs.Options;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.Core.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApp.BL.ExternalServices.Implements;

public class JwtService : IJwtService
{
    private readonly JwtOptions _opt;
    public JwtService(IOptions<JwtOptions> opt)
    {
        _opt = opt.Value;
    }

    public string CreateToken(User user, int hours = 36)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, Convert.ToString(user.Role)),
            new Claim("fullname", user.Name + " " + user.Surname)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.SecretKey));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwt = new(
            issuer: _opt.Issuer,
            audience: _opt.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddHours(hours),
            signingCredentials: credentials 
            );

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        string token = handler.WriteToken(jwt);

        return token;
    }
}
