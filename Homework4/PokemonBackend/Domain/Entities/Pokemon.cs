using Domain.Entities.Abstractions;

namespace Domain.Entities;

public class Pokemon : Entity
{
    public string Name { get; set; } = null!;
    public string? Image { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public int Hp { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
    public List<Type> Types { get; set; } = new();
    public List<Move> Moves { get; set; } = new();
    public List<Ability> Abilities { get; set; } = new();
}