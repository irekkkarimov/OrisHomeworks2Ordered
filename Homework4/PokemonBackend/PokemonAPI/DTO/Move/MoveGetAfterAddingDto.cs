using PokemonAPI.DTO.Type;

namespace PokemonAPI.DTO.Move;

public class MoveGetAfterAddingDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TypeGetDto Type { get; set; }
}