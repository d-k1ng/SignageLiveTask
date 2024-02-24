using SignageLivePlayer.Api.Data.Models;

namespace SignageLivePlayer.Api.Configuration;

public static class SeedData
{

    public static readonly User[] users = [
        new User {
            Email = "admin",
            FirstName = "",
            LastName = "",
            Password = "admin"
        }
     ];

    public static readonly UserRole[] userRoles = [
        new UserRole { UserId = "1", RoleId = "1" },
        new UserRole { UserId = "1", RoleId = "2" },
        new UserRole { UserId = "1", RoleId = "3" }
    ];


    public static readonly Site[] sites = [
        new Site() {
            Id = "1",
            SiteName = "Default"
        }
    ];

    public static readonly Player[] players = [
        new Player {
            Id = Guid.NewGuid().ToString(),
            PlayerUniqueId = Guid.NewGuid().ToString(),
            PlayerName = "Reception 1",
            SiteId = sites[0].Id,
            Site = sites[0]
        },
        new Player {
            Id = Guid.NewGuid().ToString(),
            PlayerUniqueId = Guid.NewGuid().ToString(),
            PlayerName = "Reception 2",
            SiteId = sites[0].Id,
            Site = sites[0]
        },
        new Player {
            Id = Guid.NewGuid().ToString(),
            PlayerUniqueId = Guid.NewGuid().ToString(),
            PlayerName = "Warehouse",
            SiteId = sites[0].Id,
            Site = sites[0]
        }
    ];

    

}
