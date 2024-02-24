using SignageLivePlayer.Api.Data.Models;
using System.Security.Claims;

namespace SignageLivePlayer.Api.Authentication;

public interface IJwtTokenGenerator
{
    public string GenerateToken(User user, Claim[] claims);
}
