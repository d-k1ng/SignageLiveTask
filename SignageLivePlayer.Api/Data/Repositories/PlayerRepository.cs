using Microsoft.EntityFrameworkCore;
using SignageLivePlayer.Api.Data.Db;
using SignageLivePlayer.Api.Data.Models;
using SignageLivePlayer.Api.Data.Repositories.Interfaces;
using SignageLivePlayer.Api.Data.Repositories.Responses;

namespace SignageLivePlayer.Api.Data.Repositories;

/*
 * Concrete repository class for Players
 * Injected into PlayersController
 */

public class PlayerRepository : IPlayerRepository
{
    private readonly AppDbContext _dbContext;

    public PlayerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
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
        //test if the site id exists
        //in future expand to user authorised sites
        if (_dbContext.Sites.FirstOrDefault(s => s.Id == player.SiteId) is null)
        {
            response.ErrorMessage = "Invalid Site Id";
            response.IsError = true;
            return response;
        }

        Random rnd = new();

        //create a unique id using first 6 chars of player name and random 4 digits
        string str = "" + rnd.Next(1,10000);
        str = str.PadLeft(4,char.Parse("0"));

        player.PlayerUniqueId = player.PlayerName
            .ToUpper()
            .Substring(0, Math.Min(6, player.PlayerName.Length))
            .PadRight(6, char.Parse("X"))
            + "-"
            + str;

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
