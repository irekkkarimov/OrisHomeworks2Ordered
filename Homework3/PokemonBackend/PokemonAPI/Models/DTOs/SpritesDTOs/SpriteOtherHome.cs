using Newtonsoft.Json;

namespace PokemonAPI.Models.DTOs.SpritesDTOs;

public class SpriteOtherHome
{
    [JsonProperty(PropertyName = "Front_Default")]
    public string Front_Default { get; set; } = "";
}