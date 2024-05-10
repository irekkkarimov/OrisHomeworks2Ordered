namespace PokemonAPI.Models.DTOs.ResponseDTOs;

public class PokemonResponse
{
    public List<PokemonResponseItem> Results { get; set; } = new();
    public int Count { get; set; }
}