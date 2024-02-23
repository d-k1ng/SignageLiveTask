using SignageLivePlayer.Api.Data.Db;
using SignageLivePlayer.Api.Data.Models;
using SignageLivePlayer.Api.Data.Repositories.Interfaces;

namespace SignageLivePlayer.Api.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        _dbContext.Users.Add(new User
        {
            Email = "admin@admin.admin",
            FirstName = "Admin",
            LastName = "",
            Password = "admin"
        });

        SaveChanges();
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Email == email);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}
