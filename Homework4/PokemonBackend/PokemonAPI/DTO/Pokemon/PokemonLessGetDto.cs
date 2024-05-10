using PokemonAPI.DTO.Type;

namespace PokemonAPI.DTO.Pokemon;

public class PokemonLessGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Image { get; set; }
    public List<TypeGetDto> Types { get; set; } = null!;
}