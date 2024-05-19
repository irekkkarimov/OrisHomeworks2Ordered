using AutoMapper;
using TeamHost.Application.DTOs.Category;
using TeamHost.Application.DTOs.Chats;
using TeamHost.Application.DTOs.Company;
using TeamHost.Application.DTOs.Country;
using TeamHost.Application.DTOs.Game;
using TeamHost.Application.DTOs.Platform;
using TeamHost.Application.DTOs.StaticFile;
using TeamHost.Application.DTOs.UserInfo;
using TeamHost.Application.Features.Games.Queries;
using TeamHost.Application.Features.Users.Queries;
using TeamHost.Domain.Entities;
using TeamHost.Domain.Entities.ChatEntities;
using TeamHost.Domain.Entities.GameEntities;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Game, GetGameDto>()
            .ForMember(i => i.GameId,
                i => 
                    i.MapFrom(e => e.Id))
            .ForMember(i => i.Name,
                i => i.MapFrom(e => e.Name))
            .ForMember(i => i.Description,
                i => i.MapFrom(e => e.Description))
            .ForMember(i => i.ShortDescription,
                i => i.MapFrom(e => e.ShortDescription))
            .ForMember(i => i.Price,
                i => i.MapFrom(e => e.Price))
            .ForMember(i => i.Rating,
                i => i.MapFrom(e => e.Rating))
            .ForMember(i => i.ReleaseDate,
                i => i.MapFrom(e => e.ReleaseDate))
            .ForMember(i => i.MainImage,
                i => i.MapFrom(e =>
                    e.Images.FirstOrDefault(y => y.IsMainImage)));

        CreateMap<Platform, GetPlatformDto>();

        CreateMap<Category, GetGameCategoryDto>();

        CreateMap<Country, GetGameCountryDto>();

        CreateMap<Company, GetCompanyDto>();

        CreateMap<StaticFile, GetStaticFileDto>();

        CreateMap<UserInfo, GetUserInfoResponse>();

        CreateMap<Chat, GetChatDto>();
    }
}