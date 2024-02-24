using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignageLivePlayer.Api.Data.Dtos;
using SignageLivePlayer.Api.Data.Models;
using SignageLivePlayer.Api.Data.Repositories.Interfaces;
using SignageLivePlayer.Api.Data.Repositories.Responses;

namespace SignageLivePlayer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PlayersController(IPlayerRepository _playerRepository, IMapper _mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<PlayerReadDto>> GetAll()
    {
        List<Player> players = _playerRepository.GetAll();
        List<PlayerReadDto> playerDtos = _mapper.Map<List<PlayerReadDto>>(players);
        return Ok(playerDtos);
    }

    [HttpGet("{id}", Name = "GetByPlayerId")]
    public ActionResult<PlayerReadDto> GetByPlayerId(string id)
    {

        Player? player = _playerRepository.GetByPlayerUniqueId(id);
        if (player is null) return NotFound();

        return Ok(_mapper.Map<PlayerReadDto>(player));

    }

    [HttpPost]
    public ActionResult<PlayerReadDto> CreatePlayer(PlayerCreateDto playerDto)
    {

        RepoResponse<Player> repoResponse = _playerRepository.CreatePlayer(_mapper.Map<Player>(playerDto));
        if (repoResponse.IsError) return Problem(statusCode: StatusCodes.Status400BadRequest, title: repoResponse.ErrorMessage);

        _playerRepository.SaveChanges();

        Player playerFromDb = _playerRepository.GetByPlayerUniqueId(repoResponse.Data!.PlayerUniqueId)!;

        PlayerReadDto playerDtoFromDb = _mapper.Map<PlayerReadDto>(playerFromDb);

        return CreatedAtRoute(nameof(GetByPlayerId), new {id = playerDtoFromDb.PlayerUniqueId}, playerDtoFromDb);

    }

    [HttpPut("{id}")]
    public IActionResult UpdatePlayer(string id, PlayerUpdateDto playerDto)
    {

        Player? player = _playerRepository.GetByPlayerUniqueId(id);
        if (player is null) return NotFound();

        _mapper.Map(playerDto, player);

        RepoResponse<Player> repoResponse = _playerRepository.UpdatePlayer(player);
        if (repoResponse.IsError) return Problem(statusCode: StatusCodes.Status400BadRequest, title: repoResponse.ErrorMessage);

        _playerRepository.SaveChanges();
        return NoContent();

    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        Player? player = _playerRepository.GetByPlayerUniqueId(id);
        if (player is null) return NotFound();
        _playerRepository.DeletePlayer(player);
        _playerRepository.SaveChanges();

        return NoContent();

    }


}
