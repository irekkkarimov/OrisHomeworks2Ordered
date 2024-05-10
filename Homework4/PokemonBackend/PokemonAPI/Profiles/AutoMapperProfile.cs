using AutoMapper;
using Domain.Entities;
using PokemonAPI.DTO.Ability;
using PokemonAPI.DTO.Move;
using PokemonAPI.DTO.Pokemon;
using PokemonAPI.DTO.Type;
using Type = Domain.Entities.Type;

namespace PokemonAPI.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Pokemon, PokemonGetDto>();
        CreateMap<Pokemon, PokemonLessGetDto>();
        CreateMap<Pokemon, PokemonGetAfterAddingDto>();
        CreateMap<PokemonAddDto, Pokemon>();

        CreateMap<Type, TypeGetDto>();
        CreateMap<Type, TypeGetAfterAddingDto>();
        CreateMap<TypeAddDto, Type>();

        CreateMap<Ability, AbilityGetDto>();
        CreateMap<Ability, AbilityGetAfterAddingDto>();
        CreateMap<AbilityAddDto, Ability>();

        CreateMap<Move, MoveGetDto>();
        CreateMap<Move, MoveGetAfterAddingDto>();
        CreateMap<MoveAddDto, Move>();
    }
}