using SignageLivePlayer.Api.Data.Db;
using SignageLivePlayer.Api.Data.Models;
using SignageLivePlayer.Api.Data.Repositories.Interfaces;

namespace SignageLivePlayer.Api.Data.Repositories;

public class SiteRepository(AppDbContext _dbContext) : ISiteRepository
{
    public List<Site> GetAll()
    {
        return _dbContext.Sites.ToList();
    }

    public Site GetById(string id)
    {
        return _dbContext.Sites.FirstOrDefault(s => s.Id == id) ?? new Site();
    }

    public Site CreateSite(Site site)
    {
        _dbContext.Sites.Add(site);
        return site;
    }

    public Site UpdateSite(Site site)
    {
        _dbContext.Sites.Update(site);
        return site;
    }

    public void DeleteSite(Site site)
    {
        _dbContext.Sites.Remove(site);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
    
}
