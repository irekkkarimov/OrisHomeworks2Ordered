using Shared.Enums;

namespace TeamHost.Application.DTOs.UserInfo;

public class UpdateUserInfoRequest
{
    public string? FirstName { get; set; }
    public string? Lastname { get; set; }
    public string? Bio { get; set; }
    public CountryCodes? Country { get; set; }
    public DateTime? BirthDate { get; set; }
}