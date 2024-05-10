using DataLayer.FetchDTO.Pokemon;

namespace DataLayer.FetchDTO;

public class PokeApiRequestDto
{
    public int Count { get; set; }
    public List<PokemonFetch> Results { get; init; } = new();
}