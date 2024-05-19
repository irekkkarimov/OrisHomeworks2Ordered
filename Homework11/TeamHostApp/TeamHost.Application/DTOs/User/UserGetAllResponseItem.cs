namespace TeamHost.Application.DTOs.User;

public class UserGetAllResponseItem
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsFriend { get; set; }
    
}