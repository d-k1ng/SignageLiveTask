using Microsoft.IdentityModel.Tokens;
using SignageLivePlayer.Api.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignageLivePlayer.Api.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public string GenerateToken(User user)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisisasecretforjwttokensthatwewilluse"));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        JwtSecurityToken token = new(
                                issuer: "playerapi",
                                audience: "playerclient",
                                expires: DateTime.Now.AddMinutes(30),
                                claims: claims,
                                signingCredentials: credentials
                                );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
