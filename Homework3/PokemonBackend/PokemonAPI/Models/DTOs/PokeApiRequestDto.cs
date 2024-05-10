namespace PokemonAPI.Models.DTOs;

public class PokeApiRequestDto
{
    public List<Pokemon> Results { get; init; } = new();
}