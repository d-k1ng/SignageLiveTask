using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignageLivePlayer.Api.Data.Dtos;
using SignageLivePlayer.Api.Data.Models;
using SignageLivePlayer.Api.Data.Repositories.Interfaces;

namespace SignageLivePlayer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PlayersController(ILogger<PlayersController> _logger, IPlayerRepository _playerRepository, IMapper _mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<PlayerReadDto>> GetAll()
    {
        _logger.LogInformation("Get All Called");
        List<Player> players = _playerRepository.GetAll();
        List<PlayerReadDto> playerDtos = _mapper.Map<List<PlayerReadDto>>(players);
        return Ok(playerDtos);
    }

    [HttpGet("{id}")]
    public ActionResult<PlayerReadDto> Get(string playerUniqueId)
    {

        _logger.LogInformation($"Get Id ({playerUniqueId}) Called");

        Player player = _playerRepository.GetByPlayerUniqueId(playerUniqueId);
        PlayerReadDto playerDto = _mapper.Map<PlayerReadDto>(player);

        return Ok(playerDto);

    }

    [HttpPost]
    public ActionResult<PlayerReadDto> CreatePlayer(PlayerCreateDto playerDto)
    {
        Player player = _playerRepository.CreatePlayer(_mapper.Map<Player>(playerDto));

        PlayerReadDto playerDtoFromDb = _mapper.Map<PlayerReadDto>(player);

        _playerRepository.SaveChanges();

        return Ok(playerDtoFromDb);

    }

    [HttpPut("{id}")]
    public ActionResult<PlayerReadDto> UpdatePlayer(string playerUniqueId, PlayerUpdateDto playerDto)
    {

        Player player = _playerRepository.GetByPlayerUniqueId(playerUniqueId);

        _mapper.Map(playerDto, player);

        _playerRepository.UpdatePlayer(player);

        _playerRepository.SaveChanges();

        return Ok(_mapper.Map<PlayerReadDto>(player));

    }

    [HttpDelete("{id}")]
    public void Delete(string playerUniqueId)
    {

        Player player = _playerRepository.GetByPlayerUniqueId(playerUniqueId);
        _playerRepository.DeletePlayer(player);
        _playerRepository.SaveChanges();

    }


}
