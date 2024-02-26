using AutoMapper;
using SignageLivePlayer.Api.Data.Dtos;
using SignageLivePlayer.Api.Data.Models;

namespace SignageLivePlayer.Api.Configuration;

public class MapperConfig : Profile
{
    //Configure AutoMapper
    public MapperConfig()
    {
        //Player
        CreateMap<Player, PlayerReadDto>();
        CreateMap<PlayerCreateDto, Player>();
        CreateMap<PlayerUpdateDto, Player>();

        //Site
        CreateMap<Site, SiteReadDto>();
        CreateMap<SiteCreateDto, Site>();
        CreateMap<SiteUpdateDto, Site>();
    }
}
