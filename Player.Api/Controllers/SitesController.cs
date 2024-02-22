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
public class SitesController(ILogger<SitesController> _logger, ISiteRepository _siteRepository, IMapper _mapper) : ControllerBase
{
   
    [HttpGet]
    public ActionResult<List<SiteReadDto>> GetAll()
    {
        _logger.LogInformation("Get All Called");
        List<Site> sites = _siteRepository.GetAll();
        List<SiteReadDto> siteDtos = _mapper.Map<List<SiteReadDto>>(sites);
        return Ok(siteDtos);
    }

    [HttpGet("{id}")]
    public ActionResult<SiteReadDto> GetById(string siteId)
    {

        _logger.LogInformation($"Get Id ({siteId}) Called");

        Site site = _siteRepository.GetById(siteId);
        SiteReadDto siteDto = _mapper.Map<SiteReadDto>(site);

        return Ok(siteDto);

    }

    [HttpPost]
    public ActionResult<SiteReadDto> CreateSite(SiteCreateDto siteDto)
    {
        Site site = _siteRepository.CreateSite(_mapper.Map<Site>(siteDto));

        SiteReadDto siteDtoFromDb = _mapper.Map<SiteReadDto>(site);

        _siteRepository.SaveChanges();

        return Ok(siteDtoFromDb);

    }

    [HttpPut("{id}")]
    public ActionResult<SiteReadDto> UpdateSite(string siteId, SiteUpdateDto siteDto)
    {

        Site site = _siteRepository.GetById(siteId);

        _mapper.Map(siteDto, site);

        _siteRepository.UpdateSite(site);

        _siteRepository.SaveChanges();

        return Ok(_mapper.Map<SiteReadDto>(site));

    }

    [HttpDelete("{id}")]
    public void Delete(string siteId)
    {

        Site site = _siteRepository.GetById(siteId);
        _siteRepository.DeleteSite(site);
        _siteRepository.SaveChanges();

    }


}
