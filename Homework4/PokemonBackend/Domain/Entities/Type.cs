using Domain.Entities.Abstractions;

namespace Domain.Entities;

public class Type : Entity
{
    public string Name { get; set; }
    public List<Pokemon> Pokemons { get; set; }
}