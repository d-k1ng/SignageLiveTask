using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignageLivePlayer.Api.Configuration;
using SignageLivePlayer.Api.Data.Dtos;
using SignageLivePlayer.Api.Data.Models;
using SignageLivePlayer.Api.Data.Repositories.Interfaces;
using SignageLivePlayer.Api.Data.Repositories.Responses;

namespace SignageLivePlayer.Api.Controllers;

/*
 *  /api/Players
 *  GET     GetAll()                ROLE_USER
 *  GET     GetbyPlayerId(id)       ROLE_USER
 *  POST    CreatePlayer(player)    ROLE_SITEADMIN
 *  PUT     UpdatePlayer(player)    ROLE_SITEADMIN
 *  DELETE  DeletePlayer(id)        ROLE_ADMIN
 */

[Route("api/[controller]")]
[ApiController]
public class PlayersController(IPlayerRepository _playerRepository, IMapper _mapper) : ControllerBase
{
    [Authorize(Roles = StaticData.ROLE_USER)]
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Player> players = _playerRepository.GetAll();
        List<PlayerReadDto> playerDtos = _mapper.Map<List<PlayerReadDto>>(players);
        return Ok(playerDtos);
    }

    [Authorize(Roles = StaticData.ROLE_USER)]
    [HttpGet("{id}", Name = "GetByPlayerId")]
    public IActionResult GetByPlayerId(string id)
    {

        Player? player = _playerRepository.GetByPlayerUniqueId(id);
        if (player is null) return NotFound();

        return Ok(_mapper.Map<PlayerReadDto>(player));

    }

    [Authorize(Roles = StaticData.ROLE_SITEADMIN)]
    [HttpPost]
    public IActionResult CreatePlayer(PlayerCreateDto playerDto)
    {

        RepoResponse<Player> repoResponse = _playerRepository.CreatePlayer(_mapper.Map<Player>(playerDto));
        if (repoResponse.IsError) return Problem(statusCode: StatusCodes.Status400BadRequest, title: repoResponse.ErrorMessage);

        _playerRepository.SaveChanges();

        Player playerFromDb = _playerRepository.GetByPlayerUniqueId(repoResponse.Data!.PlayerUniqueId)!;

        PlayerReadDto playerDtoFromDb = _mapper.Map<PlayerReadDto>(playerFromDb);

        return CreatedAtRoute(nameof(GetByPlayerId), new {id = playerDtoFromDb.PlayerUniqueId}, playerDtoFromDb);

    }

    [Authorize(Roles = StaticData.ROLE_SITEADMIN)]
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

    [Authorize(Roles = StaticData.ROLE_ADMIN)]
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
