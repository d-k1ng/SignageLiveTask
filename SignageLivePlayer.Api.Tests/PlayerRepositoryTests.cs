using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignageLivePlayer.Api.Configuration;
using SignageLivePlayer.Api.Controllers;
using SignageLivePlayer.Api.Data.Db;
using SignageLivePlayer.Api.Data.Dtos;
using SignageLivePlayer.Api.Data.Repositories;

namespace SignageLivePlayer.Api.Tests;

public class PlayerRepositoryTests
{
    private AppDbContext context;
    private PlayerRepository repository;

    public static DbContextOptions<AppDbContext> dbContextOptions { get; set; }

    static PlayerRepositoryTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("testDb").Options;
    }

    //Generate a new repository instance with in memory db context
    public PlayerRepositoryTests()
    {
        context = new AppDbContext(dbContextOptions);
        repository = new PlayerRepository(context);
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
    public void Task_GetPlayerById_Return_OkResult()
    {
        //Arrange
        var controller = new PlayersController(repository, GetMapper());
        var playerId = "RECEPT-0987";

        //Act
        var data = controller.GetByPlayerId(playerId);

        //Assert
        Assert.IsType<OkObjectResult>(data);
    }

    [Fact]
    public void Task_GetPlayerById_Return_NotFoundResult()
    {
        //Arrange  
        var controller = new PlayersController(repository, GetMapper());
        var playerId = "THISDOESNTEXIST";

        //Act  
        var data = controller.GetByPlayerId(playerId);

        //Assert  
        Assert.IsType<NotFoundResult>(data);
    }

    [Fact]
    public void Task_GetPlayerById_MatchResult()
    {
        //Arrange  
        var controller = new PlayersController(repository, GetMapper());
        var playerId = "RECEPT-0987";

        //Act  
        var data = controller.GetByPlayerId(playerId);

        //Assert  
        Assert.IsType<OkObjectResult>(data);

        var okResult = data as OkObjectResult;
        var player = okResult!.Value as PlayerReadDto;

        Assert.Equal("Reception Large Screen", player!.PlayerName);
        Assert.Equal(60, player.CheckInFrequency);
    }

    #endregion

    #region Get All  

    [Fact]
    public void Task_GetPlayers_Return_OkResult()
    {
        //Arrange  
        var controller = new PlayersController(repository, GetMapper());

        //Act  
        var data = controller.GetAll();

        //Assert  
        Assert.IsType<OkObjectResult>(data);
    }

    [Fact]
    public void Task_GetPlayers_MatchResult()
    {
        //Arrange  
        var controller = new PlayersController(repository, GetMapper());

        //Act  
        var data = controller.GetAll();

        //Assert  
        Assert.IsType<OkObjectResult>(data);

        var okResult = data as OkObjectResult;
        var player = okResult!.Value as List<PlayerReadDto>;

        Assert.Equal("Reception Large Screen", player![0].PlayerName);
        Assert.Equal(60, player[0].CheckInFrequency);

        Assert.Equal("Reception Small Screen 1", player[1].PlayerName);
        Assert.Equal(60, player[1].CheckInFrequency);

    }

    #endregion

    #region Add New Player 

    [Fact]
    public void Task_Add_ValidData_Return_OkResult()
    {
        //Arrange  
        var controller = new PlayersController(repository, GetMapper());
        var playerCreateDto = new PlayerCreateDto() { PlayerName = "Dining Room Main Screen", CheckInFrequency = 20, SiteId = context.Sites.First(s=>s.SiteName== "Headquarters").Id };

        //Act  
        var data = controller.CreatePlayer(playerCreateDto);

        //Assert  
        Assert.IsType<CreatedAtRouteResult>(data);
    }

    [Fact]
    public void Task_Add_InvalidSiteId_Return_ProblemDetails()
    {
        //Arrange  
        var controller = new PlayersController(repository, GetMapper());
        var playerCreateDto = new PlayerCreateDto() { PlayerName = "Dining Room Main Screen", CheckInFrequency = 20, SiteId = "thiswontexist" };

        //Act  
        var data = controller.CreatePlayer(playerCreateDto);

        //Assert  
        Assert.IsType<ObjectResult>(data);
        var objectResult = data as ObjectResult;
        Assert.IsType<ProblemDetails>(objectResult!.Value);
        var createResult = objectResult!.Value as ProblemDetails;
        Assert.NotNull(createResult);
        Assert.Equal("Invalid Site Id", createResult.Title); //ideally this would not be hardcoded and use a specific exception
    }

    [Fact]
    public void Task_Add_ValidData_MatchResult()
    {
        //Arrange  
        var controller = new PlayersController(repository, GetMapper());
        var playerCreateDto = new PlayerCreateDto() { PlayerName = "Dining Room Second Screen", CheckInFrequency = 20, SiteId = context.Sites.First(s => s.SiteName == "Headquarters").Id };

        //Act  
        var data = controller.CreatePlayer(playerCreateDto);

        //Assert  
        Assert.IsType<CreatedAtRouteResult>(data);

        var okResult = data as CreatedAtRouteResult;
        var player = okResult!.Value as PlayerReadDto;

        Assert.Equal("Dining Room Second Screen", player!.PlayerName);
        Assert.Equal(20, player!.CheckInFrequency);
    }

    #endregion

    #region Update Existing Player  

    [Fact]
    public void Task_Update_ValidData_Return_OkResult()
    {
        //Arrange  
        var controller = new PlayersController(repository, GetMapper());
        var playerId = "RECEPT-0987";

        //Act  
        var existingPlayer = controller.GetByPlayerId(playerId);
        var okResult = existingPlayer as OkObjectResult;
        var result = okResult!.Value as PlayerReadDto;


        var player = new PlayerUpdateDto();
        player.PlayerName = "Player Updated";
        player.CheckInFrequency = result!.CheckInFrequency;
        player.SiteId = result!.Site!.Id;

        var updatedData = controller.UpdatePlayer(playerId, player);

        //Assert  
        Assert.IsType<NoContentResult>(updatedData);
    }

    [Fact]
    public void Task_Update_InvalidSiteId_Return_ProblemDetails()
    {
        //Arrange  
        var controller = new PlayersController(repository, GetMapper());
        var playerId = "RECEPT-0987";

        //Act  
        var existingPlayer = controller.GetByPlayerId(playerId);
        var okResult = existingPlayer as OkObjectResult;
        var result = okResult!.Value as PlayerReadDto;

        var player = new PlayerUpdateDto();
        player.PlayerName = "Player Updated";
        player.CheckInFrequency = result!.CheckInFrequency;
        player.SiteId = "notthis";

        var data = controller.UpdatePlayer(playerId, player);
        var objectResult = data as ObjectResult;
        

        //Assert  
        Assert.IsType<ObjectResult>(data);
        Assert.IsType<ProblemDetails>(objectResult!.Value);
        var updateResult = objectResult!.Value as ProblemDetails;
        Assert.NotNull(updateResult);
        Assert.Equal("Invalid Site Id", updateResult.Title); //ideally this would not be hardcoded and use a specific exception
    }

    [Fact]
    public void Task_Update_InvalidData_Return_NotFound()
    {
        //Arrange  
        var controller = new PlayersController(repository, GetMapper());
        var playerId = "WONTBETHIS";

        //Act  
        var existingPlayer = controller.GetByPlayerId("RECEPT-0987");
        var okResult = existingPlayer as OkObjectResult;
        var result = okResult!.Value as PlayerReadDto;

        var player = new PlayerUpdateDto();
        player.PlayerName = "Player Updated";
        player.CheckInFrequency = result!.CheckInFrequency;
        player.SiteId = result!.Site!.Id;

        var data = controller.UpdatePlayer(playerId, player);

        //Assert  
        Assert.IsType<NotFoundResult>(data);
    }

    #endregion

    #region Delete Player  

    [Fact]
    public void Task_Delete_Player_Return_NoContentResult()
    {
        //Arrange  
        var controller = new PlayersController(repository, GetMapper());
        var playerId = "RECEPT-0987";

        //Act  
        var data = controller.Delete(playerId);

        //Assert  
        Assert.IsType<NoContentResult>(data);
    }

    [Fact]
    public void Task_Delete_Player_Return_NotFoundResult()
    {
        //Arrange  
        var controller = new PlayersController(repository, GetMapper());
        var playerId = "THISWONTEXIST";

        //Act  
        var data = controller.Delete(playerId);

        //Assert  
        Assert.IsType<NotFoundResult>(data);
    }

    [Fact]
    public void Task_Delete_NullId_Return_NotFoundResult()
    {
        //Arrange  
        var controller = new PlayersController(repository, GetMapper());
        string? playerId = null;

        //Act  
        var data = controller.Delete(playerId!);

        //Assert  
        Assert.IsType<NotFoundResult>(data);
    }

    #endregion

}
