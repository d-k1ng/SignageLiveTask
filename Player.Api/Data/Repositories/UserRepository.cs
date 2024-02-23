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

            _dbContext.Roles.AddRange([
                new Role { Id = "1", RoleName = "ADMIN" },
                new Role { Id = "2", RoleName = "SITEADMIN" },
                new Role { Id = "3", RoleName = "USER" }
            ]);

            _dbContext.Users.Add(new User
            {
                Email = "admin@admin.admin",
                FirstName = "Admin",
                LastName = "",
                Password = "admin"
            });

            _dbContext.UserRoles.AddRange([
                new UserRole { UserId = "1", RoleId = "1" },
                new UserRole { UserId = "1", RoleId = "2" },
                new UserRole { UserId = "1", RoleId = "3" }
            ]);

            SaveChanges();
        }
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
