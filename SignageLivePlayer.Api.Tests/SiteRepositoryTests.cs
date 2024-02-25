using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignageLivePlayer.Api.Configuration;
using SignageLivePlayer.Api.Controllers;
using SignageLivePlayer.Api.Data.Db;
using SignageLivePlayer.Api.Data.Dtos;
using SignageLivePlayer.Api.Data.Repositories;

namespace SignageLivePlayer.Api.Tests;

public class SiteRepositoryTests
{
    private AppDbContext context;
    private SiteRepository repository;

    public static DbContextOptions<AppDbContext> dbContextOptions { get; set; }

    static SiteRepositoryTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("testDb").Options;
    }

    //Generate a new repository instance with in memory db context
    public SiteRepositoryTests()
    {
        context = new AppDbContext(dbContextOptions);
        repository = new SiteRepository(context);
        SeedTestData(context);
    }

    //Generate Seed Data
    private void SeedTestData(AppDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.SaveChanges();

    }

    private IMapper GetMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        var mapper = config.CreateMapper();
        return mapper;

    }



    #region Get By Id  

    [Fact]
    public void Task_GetSiteById_Return_OkResult()
    {
        //Arrange
        var controller = new SitesController(repository, GetMapper());
        var siteId = context.Sites.First(s => s.SiteName == "Headquarters").Id;

        //Act
        var data = controller.GetById(siteId);

        //Assert
        Assert.IsType<OkObjectResult>(data);
    }

    [Fact]
    public void Task_GetSiteById_Return_NotFoundResult()
    {
        //Arrange  
        var controller = new SitesController(repository, GetMapper());
        var siteId = "THISDOESNTEXIST";

        //Act  
        var data = controller.GetById(siteId);

        //Assert  
        Assert.IsType<NotFoundResult>(data);
    }

    [Fact]
    public void Task_GetSiteById_MatchResult()
    {
        //Arrange  
        var controller = new SitesController(repository, GetMapper());
        var siteId = context.Sites.First(s => s.SiteName == "Headquarters").Id;

        //Act  
        var data = controller.GetById(siteId);

        //Assert  
        Assert.IsType<OkObjectResult>(data);

        var okResult = data as OkObjectResult;
        var site = okResult!.Value as SiteReadDto;

        Assert.Equal("Headquarters", site!.SiteName);
        Assert.Equal("30 South Street", site!.SiteAddress1);
    }

    #endregion

    #region Get All  

    [Fact]
    public void Task_GetSites_Return_OkResult()
    {
        //Arrange  
        var controller = new SitesController(repository, GetMapper());

        //Act  
        var data = controller.GetAll();

        //Assert  
        Assert.IsType<OkObjectResult>(data);
    }

    [Fact]
    public void Task_GetSites_MatchResult()
    {
        //Arrange  
        var controller = new SitesController(repository, GetMapper());

        //Act  
        var data = controller.GetAll();

        //Assert  
        Assert.IsType<OkObjectResult>(data);

        var okResult = data as OkObjectResult;
        var site = okResult!.Value as List<SiteReadDto>;

        Assert.Equal("Headquarters", site![0].SiteName);
        Assert.Equal("30 South Street", site![0].SiteAddress1);

        Assert.Equal("London Branch", site![1].SiteName);
        Assert.Equal("58 Grove Road", site![1].SiteAddress1);

    }

    #endregion

    #region Add New Site

    [Fact]
    public void Task_Add_ValidData_Return_OkResult()
    {
        //Arrange  
        var controller = new SitesController(repository, GetMapper());
        var siteCreateDto = new SiteCreateDto()
        {
            SiteName = "Stevenage Branch",
            SiteAddress1 = "34 North Street",
            SiteTown = "Stevenage",
            SitePostcode = "SG1 7HJ"
        };

        //Act  
        var data = controller.CreateSite(siteCreateDto);

        //Assert  
        Assert.IsType<CreatedAtRouteResult>(data);
    }

    [Fact]
    public void Task_Add_ValidData_MatchResult()
    {
        //Arrange  
        var controller = new SitesController(repository, GetMapper());
        var siteCreateDto = new SiteCreateDto()
        {
            SiteName = "Stevenage Branch",
            SiteAddress1 = "34 North Street",
            SiteTown = "Stevenage",
            SitePostcode = "SG1 7HJ"
        };

        //Act  
        var data = controller.CreateSite(siteCreateDto);

        //Assert  
        Assert.IsType<CreatedAtRouteResult>(data);

        var okResult = data as CreatedAtRouteResult;
        var site = okResult!.Value as SiteReadDto;

        Assert.Equal("Stevenage Branch", site!.SiteName);
        Assert.Equal("34 North Street", site!.SiteAddress1);
        Assert.Equal("Stevenage", site!.SiteTown);
        Assert.Equal("SG1 7HJ", site!.SitePostcode);
    }

    #endregion

    #region Update Existing Site 

    [Fact]
    public void Task_Update_ValidData_Return_OkResult()
    {
        //Arrange  
        var controller = new SitesController(repository, GetMapper());
        var siteId = context.Sites.First(s => s.SiteName == "Headquarters").Id;

        //Act  
        var existingSite = controller.GetById(siteId);
        var okResult = existingSite as OkObjectResult;
        var result = okResult!.Value as SiteReadDto;


        var site = new SiteUpdateDto();
        site.SiteName = "New HQ";
        site.SiteAddress1 = result!.SiteAddress1;
        site.SiteTown = result.SiteTown;
        site.SitePostcode = result.SitePostcode;

        var updatedData = controller.UpdateSite(siteId, site);

        //Assert  
        Assert.IsType<NoContentResult>(updatedData);
    }

    [Fact]
    public void Task_Update_InvalidData_Return_NotFound()
    {
        //Arrange  
        var controller = new SitesController(repository, GetMapper());
        var siteId = "WONTBETHIS";

        //Act  
        var existingSite = controller.GetById(context.Sites.First(s => s.SiteName == "Headquarters").Id);
        var okResult = existingSite as OkObjectResult;
        var result = okResult!.Value as SiteReadDto;

        var site = new SiteUpdateDto();
        site.SiteName = "New HQ";
        site.SiteAddress1 = result!.SiteAddress1;
        site.SiteTown = result.SiteTown;
        site.SitePostcode = result.SitePostcode;

        var data = controller.UpdateSite(siteId, site);

        //Assert  
        Assert.IsType<NotFoundResult>(data);
    }

    #endregion

    #region Delete Site

    [Fact]
    public void Task_Delete_Site_Return_NoContentResult()
    {
        //Arrange  
        var controller = new SitesController(repository, GetMapper());
        var siteId = context.Sites.First(s => s.SiteName == "Headquarters").Id;

        //Act  
        var data = controller.Delete(siteId);

        //Assert  
        Assert.IsType<NoContentResult>(data);
    }

    [Fact]
    public void Task_Delete_Site_Return_NotFoundResult()
    {
        //Arrange  
        var controller = new SitesController(repository, GetMapper());
        var siteId = "THISWONTEXIST";

        //Act  
        var data = controller.Delete(siteId);

        //Assert  
        Assert.IsType<NotFoundResult>(data);
    }

    [Fact]
    public void Task_Delete_NullId_Return_NotFoundResult()
    {
        //Arrange  
        var controller = new SitesController(repository, GetMapper());
        string? siteId = null;

        //Act  
        var data = controller.Delete(siteId!);

        //Assert  
        Assert.IsType<NotFoundResult>(data);
    }

    #endregion

}
