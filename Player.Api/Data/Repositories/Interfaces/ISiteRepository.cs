using SignageLivePlayer.Api.Data.Models;

namespace SignageLivePlayer.Api.Data.Repository.Interfaces;

public interface ISiteRepository
{
    public List<Site> GetAll();

    public Site GetById(string id);

    public Site CreateSite(Site site);

    public Site UpdateSite(Site site);

    public void DeleteSite(Site site);

    public void SaveChanges();
}
