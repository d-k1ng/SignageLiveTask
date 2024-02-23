using SignageLivePlayer.Api.Data.Models;

namespace SignageLivePlayer.Api.Data.Repositories.Interfaces;

public interface IUserRepository
{
    public void Add(User user);

    public User? GetUserByEmail(string email);

    public void SaveChanges();
}
