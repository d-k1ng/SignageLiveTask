using SignageLivePlayer.Api.Data.Models;

namespace SignageLivePlayer.Api.Data.Repositories.Interfaces;

public interface IPlayerRepository
{
    public List<Player> GetAll();

    public Player GetByPlayerUniqueId(string id);

    public Player CreatePlayer(Player player);

    public Player UpdatePlayer(Player player);

    public void DeletePlayer(Player player);

    public void SaveChanges();
}
