using PokemonAPI.DTO.Ability;
using PokemonAPI.DTO.Move;
using PokemonAPI.DTO.Type;

namespace PokemonAPI.DTO.Pokemon;

public class PokemonGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Image { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public int Hp { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
    public List<TypeGetDto> Types { get; set; } = new();
    public List<MoveGetDto> Moves { get; set; } = new();
    public List<AbilityGetDto> Abilities { get; set; } = new();
}