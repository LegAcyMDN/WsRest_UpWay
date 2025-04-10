using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class CompteClient
{
    public string FullName => NomClient + " " + PrenomClient;

    public string GenerateJwtToken(IConfiguration config)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT_SECRET_KEY"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, EmailClient),
            new Claim(JwtRegisteredClaimNames.Sid, ClientId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, FullName),
            new Claim("role", Usertype),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            config["JWT_ISSUER"],
            config["JWT_AUDIENCE"],
            claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}