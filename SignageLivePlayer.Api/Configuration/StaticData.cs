using SignageLivePlayer.Api.Data.Models;

namespace SignageLivePlayer.Api.Configuration;

//Hardcoded Role Data
public static class StaticData
{
    public const string ROLE_ADMIN = "ADMIN";
    public const string ROLE_SITEADMIN = "SITEADMIN";
    public const string ROLE_USER = "USER";

    public static readonly Role[] roles = [
        new Role { Id = "1", RoleName = ROLE_ADMIN },
        new Role { Id = "2", RoleName = ROLE_SITEADMIN },
        new Role { Id = "3", RoleName = ROLE_USER }
    ];

    public static Role[] AllRoles()
    {
        return roles;
    }
}
