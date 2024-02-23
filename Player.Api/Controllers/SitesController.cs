using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignageLivePlayer.Api.Data.Dtos;
using SignageLivePlayer.Api.Data.Models;
using SignageLivePlayer.Api.Data.Repositories.Interfaces;
using System.Numerics;

namespace SignageLivePlayer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SitesController(ISiteRepository _siteRepository, IMapper _mapper) : ControllerBase
{
   
    [HttpGet]
    public ActionResult<List<SiteReadDto>> GetAll()
    {
        List<Site> sites = _siteRepository.GetAll();
        List<SiteReadDto> siteDtos = _mapper.Map<List<SiteReadDto>>(sites);
        return Ok(siteDtos);
    }

    [HttpGet("{id}", Name = "GetById")]
    public ActionResult<SiteReadDto> GetById(string id)
    {

        Site? site = _siteRepository.GetById(id);
        if (site is null) return NotFound();
        SiteReadDto siteDto = _mapper.Map<SiteReadDto>(site);

        return Ok(siteDto);

    }

    [HttpPost]
    public ActionResult<SiteReadDto> CreateSite(SiteCreateDto siteDto)
    {
        Site site = _siteRepository.CreateSite(_mapper.Map<Site>(siteDto));
        _siteRepository.SaveChanges();

        SiteReadDto siteDtoFromDb = _mapper.Map<SiteReadDto>(site);
        return CreatedAtRoute(nameof(GetById), new { id = siteDtoFromDb.Id }, siteDtoFromDb);

    }

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
