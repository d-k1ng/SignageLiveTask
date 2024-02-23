using SignageLivePlayer.Api.Data.Models;

namespace SignageLivePlayer.Api.Authentication;

public interface IJwtTokenGenerator
{
    public string GenerateToken(User user);
}
