using PokemonAPI.DTO.Pokemon;
using PokemonAPI.Models;
using PokemonAPI.Models.DTOs.ResponseDTOs;

namespace PokemonAPI.Services.PokemonService;

public interface IPokemonService
{
    Task<PokemonLessListGetDto> GetAllAsync(int limit, int offset);
    Task<PokemonLessListGetDto> GetByFilterAsync(string filter, int limit, int offset);
    Task<PokemonGetDto> GetByIdOrNameAsync(string idOrName);
}