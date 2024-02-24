using Microsoft.IdentityModel.Tokens;
using SignageLivePlayer.Api.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignageLivePlayer.Api.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public string GenerateToken(User user, Claim[] claims)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisisasecretforjwttokensthatwewilluse"));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

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
