using PokemonAPI.DTO.Type;

namespace PokemonAPI.DTO.Move;

public class MoveGetDto
{
    public string Name { get; set; } = null!;
    public TypeGetDto Type { get; set; }
}