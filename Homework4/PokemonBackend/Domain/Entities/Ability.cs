using Domain.Entities.Abstractions;

namespace Domain.Entities;

public class Ability : Entity
{
    public string Name { get; set; }
    public List<Pokemon> Pokemons { get; set; }
}