using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignageLivePlayer.Api.Configuration;
using SignageLivePlayer.Api.Data.Dtos;
using SignageLivePlayer.Api.Data.Models;
using SignageLivePlayer.Api.Data.Repositories.Interfaces;

namespace SignageLivePlayer.Api.Controllers;

/*
 *  /api/Sites
 *  GET     GetAll()            ROLE_USER
 *  GET     GetbyId(id)         ROLE_USER
 *  POST    CreateSite(site)    ROLE_SITEADMIN
 *  PUT     UpdateSite(site)    ROLE_SITEADMIN
 *  DELETE  DeleteSite(id)      ROLE_ADMIN
 */

[Route("api/[controller]")]
[ApiController]
public class SitesController(ISiteRepository _siteRepository, IMapper _mapper) : ControllerBase
{
    [Authorize(Roles = StaticData.ROLE_USER)]
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Site> sites = _siteRepository.GetAll();
        List<SiteReadDto> siteDtos = _mapper.Map<List<SiteReadDto>>(sites);
        return Ok(siteDtos);
    }

    [Authorize(Roles = StaticData.ROLE_USER)]
    [HttpGet("{id}", Name = "GetById")]
    public IActionResult GetById(string id)
    {

        Site? site = _siteRepository.GetById(id);
        if (site is null) return NotFound();
        SiteReadDto siteDto = _mapper.Map<SiteReadDto>(site);

        return Ok(siteDto);

    }

    [Authorize(Roles = StaticData.ROLE_SITEADMIN)]
    [HttpPost]
    public IActionResult CreateSite(SiteCreateDto siteDto)
    {
        Site site = _siteRepository.CreateSite(_mapper.Map<Site>(siteDto));
        _siteRepository.SaveChanges();

        SiteReadDto siteDtoFromDb = _mapper.Map<SiteReadDto>(site);
        return CreatedAtRoute(nameof(GetById), new { id = siteDtoFromDb.Id }, siteDtoFromDb);

    }

    [Authorize(Roles = StaticData.ROLE_SITEADMIN)]
    [HttpPut("{id}")]
    public IActionResult UpdateSite(string id, SiteUpdateDto siteDto)
    {

        Site? site = _siteRepository.GetById(id);
        if (site is null) return NotFound();

        _mapper.Map(siteDto, site);

        _siteRepository.UpdateSite(site);

        _siteRepository.SaveChanges();

        return NoContent();

    }

    [Authorize(Roles = StaticData.ROLE_ADMIN)]
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {

        Site? site = _siteRepository.GetById(id);
        if (site is null) return NotFound();
       

        _siteRepository.DeleteSite(site);
        _siteRepository.SaveChanges();
        return NoContent();

    }


}
