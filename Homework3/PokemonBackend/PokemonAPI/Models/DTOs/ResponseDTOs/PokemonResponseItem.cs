using System.Reflection;

namespace PokemonAPI.Models.DTOs.ResponseDTOs;

public class PokemonResponseItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public SpriteInfoDto Sprites { get; set; } = null!;
    public List<TypeInfoDto> Types { get; set; } = null!;
}