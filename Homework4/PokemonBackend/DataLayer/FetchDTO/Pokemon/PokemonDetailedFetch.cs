using DataLayer.FetchDTO.Ability;
using DataLayer.FetchDTO.Move;
using DataLayer.FetchDTO.Sprite;
using DataLayer.FetchDTO.Stat;
using DataLayer.FetchDTO.Types;

namespace DataLayer.FetchDTO.Pokemon;

public class PokemonDetailedFetch
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Height { get; set; }
    public int Weight { get; set; }
    public List<StatInfoDto> Stats { get; set; }
    public SpriteInfoDto Sprites { get; set; } = null!;
    public List<TypeInfoDto> Types { get; set; } = new();
    public List<MoveInfoDto> Moves { get; set; } = new();
    public List<AbilityInfoDto> Abilities { get; set; } = new();
}