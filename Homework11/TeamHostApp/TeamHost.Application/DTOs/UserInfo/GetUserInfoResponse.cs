namespace TeamHost.Application.DTOs.UserInfo;

public class GetUserInfoResponse
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Bio { get; set; }
    public Domain.Entities.GameEntities.Country? Country { get; set; }
    public DateTime? BirthDate { get; set; }
}