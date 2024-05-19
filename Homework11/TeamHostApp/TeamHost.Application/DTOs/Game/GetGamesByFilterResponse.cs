using Redis.OM.Modeling;

namespace TeamHost.Application.DTOs.Game;

[Document(StorageType = StorageType.Json)]
public class GetGamesByFilterResponse
{
    [RedisIdField]
    [Indexed]
    public string? Filter { get; set; }
    public List<GetGameDto> Games { get; set; } = new();
} 