using SignageLivePlayer.Api.Data.Db;
using SignageLivePlayer.Api.Data.Models;
using SignageLivePlayer.Api.Data.Repositories.Interfaces;

namespace SignageLivePlayer.Api.Data.Repositories;

public class PlayerRepository(AppDbContext _dbContext) : IPlayerRepository
{
    public List<Player> GetAll()
    {
        return _dbContext.Players.ToList();
    }

    public Player GetByPlayerUniqueId(string id)
    {
        return _dbContext.Players.FirstOrDefault(x => x.PlayerUniqueId == id) ?? new Player();
    }

    public Player CreatePlayer(Player player)
    {
        _dbContext.Players.Add(player);
        return player;
    }

    public Player UpdatePlayer(Player player)
    {
        _dbContext.Players.Update(player);
        return player;
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
