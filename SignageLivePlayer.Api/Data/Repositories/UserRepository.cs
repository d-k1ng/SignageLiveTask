using SignageLivePlayer.Api.Configuration;
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

        if (!_dbContext.Users.Any())
        {
            foreach (var role in Roles.AllRoles()) _dbContext.Roles.Add(role);
            foreach (var user in SeedData.users) _dbContext.Users.Add(user);
            foreach (var userRole in SeedData.userRoles) _dbContext.UserRoles.Add(userRole);
            SaveChanges();
        }
    }

    public void Add(User user)
    {
        //add user
        _dbContext.Users.Add(user);

        //add user role USER for all new users
        foreach (var role in Roles.AllRoles())
        {
            if (role.RoleName == Roles.ROLE_USER) {
                UserRole userRole = new UserRole();
                userRole.UserId = user.Id;
                userRole.RoleId = role.Id;
                _dbContext.UserRoles.Add(userRole);
            }
        }
        
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
