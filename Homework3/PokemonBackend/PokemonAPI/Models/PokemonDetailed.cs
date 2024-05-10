using PokemonAPI.Models.DTOs;
using Redis.OM.Modeling;

namespace PokemonAPI.Models;

[Document(StorageType = StorageType.Json)]
public class PokemonDetailed
{
    [RedisIdField] public int Id { get; set; }
    [Indexed] public string Name { get; set; } = "";
    public int Height { get; set; }
    public int Weight { get; set; }
    public List<MoveInfoDto> Moves { get; set; } = null!;
    public SpriteInfoDto Sprites { get; set; } = null!;
    public List<AbilityInfoDto> Abilities { get; set; } = null!;
    public List<TypeInfoDto> Types { get; set; } = null!;
    public List<StatInfoDto> Stats { get; set; } = null!;
}