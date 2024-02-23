using Microsoft.EntityFrameworkCore;
using SignageLivePlayer.Api.Data.Db;
using SignageLivePlayer.Api.Data.Models;
using SignageLivePlayer.Api.Data.Repositories.Interfaces;
using SignageLivePlayer.Api.Data.Repositories.Responses;

namespace SignageLivePlayer.Api.Data.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly AppDbContext _dbContext;

    public PlayerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        if (!_dbContext.Sites.Any()) SeedData();
        
    }

    public void SeedData()
    {
        Site site = new Site
        {
            Id = "1",
            SiteName = "Default"
        };
        _dbContext.Sites.Add(site);

        _dbContext.Players.Add(new Player
        {
            Id = Guid.NewGuid().ToString(),
            PlayerUniqueId = Guid.NewGuid().ToString(),
            PlayerName = "Reception 1",
            SiteId = site.Id,
            Site = site
        });

        _dbContext.Players.Add(new Player
        {
            Id = Guid.NewGuid().ToString(),
            PlayerUniqueId = Guid.NewGuid().ToString(),
            PlayerName = "Reception 2",
            SiteId = site.Id,
            Site = site
        });

        _dbContext.Players.Add(new Player
        {
            Id = Guid.NewGuid().ToString(),
            PlayerUniqueId = Guid.NewGuid().ToString(),
            PlayerName = "Warehouse",
            SiteId = site.Id,
            Site = site
        });

        SaveChanges();
    }

    public List<Player> GetAll()
    {
        return _dbContext.Players.Include("Site").ToList();
    }

    public Player? GetByPlayerUniqueId(string id)
    {
        return _dbContext.Players.Include("Site").FirstOrDefault(x => x.PlayerUniqueId == id);
    }

    public RepoResponse<Player> CreatePlayer(Player player)
    {
        RepoResponse<Player> response = new();
        //test if the site id exist
        //in future expand to user authorised sites
        if (_dbContext.Sites.FirstOrDefault(s => s.Id == player.SiteId) is null)
        {
            response.ErrorMessage = "Invalid Site Id";
            response.IsError = true;
            return response;
        }
        _dbContext.Players.Add(player);
        response.Data = player;
        response.IsError = false;
        return response;
    }

    public RepoResponse<Player> UpdatePlayer(Player player)
    {
        RepoResponse<Player> response = new();
        if (_dbContext.Sites.FirstOrDefault(s => s.Id == player.SiteId) is null)
        {
            response.ErrorMessage = "Invalid Site Id";
            response.IsError = true;
            return response;
        }
        _dbContext.Players.Update(player);
        response.Data = player;
        response.IsError = false;
        return response;
    }

    public void DeletePlayer(Player player)
    {
        _dbContext.Players.Remove(player);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    
}
