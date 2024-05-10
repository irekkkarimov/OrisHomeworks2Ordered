using Domain.Entities.Abstractions;

namespace Domain.Entities;

public class Move : Entity
{
    public string Name { get; set; }
    public int TypeId { get; set; }
    public Type? Type { get; set; } = null!;
    public List<Pokemon> Pokemons { get; set; } = new();
}