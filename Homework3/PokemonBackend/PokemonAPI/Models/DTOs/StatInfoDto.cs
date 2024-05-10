using Newtonsoft.Json;

namespace PokemonAPI.Models.DTOs;

public class StatInfoDto
{
    [JsonProperty(PropertyName = "Base_Stat")]
    public int Base_Stat { get; set; }
    public Stat Stat { get; set; } = null!;
}