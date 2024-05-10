using TeamHost.Application.DTOs.Country;

namespace TeamHost.Application.DTOs.Company;

public class GetCompanyDto
{
    public string Name { get; set; } = null!;
    public GetGameCountryDto Country { get; set; } = null!;
}