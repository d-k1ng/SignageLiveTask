using SignageLivePlayer.Api.Data.Models;

namespace SignageLivePlayer.Api.Configuration;

//SeedData to be used within db context initialization
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
            Id = Guid.NewGuid().ToString(),
            SiteName = "Headquarters",
            SiteAddress1 = "30 South Street",
            SiteTown = "Cambridge",
            SitePostcode = "CB85 1RA"
        },
        new Site() {
            Id = Guid.NewGuid().ToString(),
            SiteName = "London Branch",
            SiteAddress1 = "58 Grove Road",
            SiteTown = "East Central London",
            SitePostcode = "CB85 1RA"
        },
        new Site() {
            Id = Guid.NewGuid().ToString(),
            SiteName = "Manchester Branch",
            SiteAddress1 = "54 York Road",
            SiteTown = "Manchester",
            SitePostcode = "M52 3RK"
        }
    ];

    public static readonly Player[] players = [
        new Player {
            Id = Guid.NewGuid().ToString(),
            PlayerUniqueId = "RECEPT-0987",
            PlayerName = "Reception Large Screen",
            CheckInFrequency = 60,
            SiteId = sites[0].Id
        },
        new Player {
            Id = Guid.NewGuid().ToString(),
            PlayerUniqueId = "RECEPT-1273",
            PlayerName = "Reception Small Screen 1",
            CheckInFrequency = 60,
            SiteId = sites[0].Id
        },
        new Player {
            Id = Guid.NewGuid().ToString(),
            PlayerUniqueId = "RECEPT-1986",
            PlayerName = "Reception Small Screen 2",
            CheckInFrequency = 60,
            SiteId = sites[0].Id
        },
        new Player {
            Id = Guid.NewGuid().ToString(),
            PlayerUniqueId = "SALESO-5459",
            PlayerName = "Sales Office",
            CheckInFrequency = 120,
            SiteId = sites[0].Id
        },
        new Player {
            Id = Guid.NewGuid().ToString(),
            PlayerUniqueId = "MARKET-2278",
            PlayerName = "Marketing Office 1",
            CheckInFrequency = 180,
            SiteId = sites[1].Id
        },
        new Player {
            Id = Guid.NewGuid().ToString(),
            PlayerUniqueId = "MARKET-3424",
            PlayerName = "Marketing Office 2",
            CheckInFrequency = 100,
            SiteId = sites[1].Id
        },
        new Player {
            Id = Guid.NewGuid().ToString(),
            PlayerUniqueId = "WAREHO-3751",
            PlayerName = "Warehouse Building 1",
            CheckInFrequency = 30,
            SiteId = sites[2].Id
        },
        new Player {
            Id = Guid.NewGuid().ToString(),
            PlayerUniqueId = "WAREHO-7364",
            PlayerName = "Warehouse Building 2",
            CheckInFrequency = 30,
            SiteId = sites[2].Id
        }
    ];

    

}
