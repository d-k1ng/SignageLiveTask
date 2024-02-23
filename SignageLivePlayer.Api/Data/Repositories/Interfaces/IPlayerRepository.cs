using SignageLivePlayer.Api.Data.Models;
using SignageLivePlayer.Api.Data.Repositories.Responses;

namespace SignageLivePlayer.Api.Data.Repositories.Interfaces;

public interface IPlayerRepository
{
    public List<Player> GetAll();

    public Player? GetByPlayerUniqueId(string id);

    public RepoResponse<Player> CreatePlayer(Player player);

    public RepoResponse<Player> UpdatePlayer(Player player);

    public void DeletePlayer(Player player);

    public void SaveChanges();
}
