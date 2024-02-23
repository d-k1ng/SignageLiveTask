using Microsoft.EntityFrameworkCore;
using SignageLivePlayer.Api.Data.Models;

namespace SignageLivePlayer.Api.Data.Db;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
}
