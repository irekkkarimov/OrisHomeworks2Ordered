using Type = DataLayer.FetchDTO.Types.Type;

namespace DataLayer.FetchDTO.Move;

public class Move
{
    public string Name { get; set; } = "";
    public string Url { get; set; } = "";
    public Type? Type { get; set; }
}