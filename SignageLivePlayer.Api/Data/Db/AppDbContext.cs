using Microsoft.EntityFrameworkCore;
using SignageLivePlayer.Api.Configuration;
using SignageLivePlayer.Api.Data.Models;

namespace SignageLivePlayer.Api.Data.Db;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<Player> Players { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Role>().HasData(StaticData.AllRoles());
        modelBuilder.Entity<User>().HasData(SeedData.users);
        modelBuilder.Entity<UserRole>().HasData(SeedData.userRoles);
        modelBuilder.Entity<Site>().HasData(SeedData.sites);
        modelBuilder.Entity<Player>().HasData(SeedData.players);

    }


}
