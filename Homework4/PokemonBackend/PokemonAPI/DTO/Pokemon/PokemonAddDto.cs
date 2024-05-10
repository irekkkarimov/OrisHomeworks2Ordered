namespace PokemonAPI.DTO.Pokemon;

public class PokemonAddDto
{
    public string Name { get; set; } = null!;
    public string? Image { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public int Hp { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
}