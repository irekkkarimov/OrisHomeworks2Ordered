using PokemonAPI.DTO.Pokemon;

namespace PokemonAPI.Models.DTOs.ResponseDTOs;

public class PokemonLessListGetDto
{
    public int Count { get; set; }
    public List<PokemonLessGetDto> Results { get; set; } = new();
}