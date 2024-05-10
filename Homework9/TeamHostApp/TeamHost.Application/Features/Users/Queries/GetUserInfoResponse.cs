using TeamHost.Domain.Entities.GameEntities;

namespace TeamHost.Application.Features.Users.Queries;

public class GetUserInfoResponse
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Bio { get; set; }
    public Country? Country { get; set; }
    public DateTime? BirthDate { get; set; }
}