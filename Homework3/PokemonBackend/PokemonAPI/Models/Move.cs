namespace PokemonAPI.Models;

public class Move
{
    public string Name { get; set; } = "";
    public string Url { get; set; } = "";
    public Type? Type { get; set; }
}