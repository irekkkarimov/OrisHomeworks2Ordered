namespace TeamHost.Application.DTOs.Country;

public class GetGameCountryDto
{
    /// <summary>
    /// Название страны
    /// </summary>
    public string Name { get; set; } = null!;
    
    /// <summary>
    /// Код страны
    /// </summary>
    public int Code { get; set; }
    
    /// <summary>
    /// 2-х буквенный код страны
    /// </summary>
    public string? Alpha2 { get; set; }
    
    /// <summary>
    /// 3-х буквенный код страны
    /// </summary>
    public string? Alpha3 { get; set; }
}