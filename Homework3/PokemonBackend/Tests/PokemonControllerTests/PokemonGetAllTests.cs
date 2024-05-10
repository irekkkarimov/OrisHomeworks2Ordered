using Newtonsoft.Json;
using PokemonAPI.Models.DTOs.ResponseDTOs;

namespace Tests.PokemonControllerTests;

[TestClass]
public class PokemonGetAllTests
{
    private readonly HttpClient _httpClient = new();
    private const string Url = "http://localhost:5254/pokemon/getall";
        
    [TestMethod]
    [DataRow(5)]
    public async Task LimitWorksRight(int limit)
    {
        // Arrange
        var specificUrl = Url + $"?limit={limit}";
        
        // Act
        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<PokemonResponse>(response);

        if (responseJson is null || !responseJson.Results.Any())
            throw new NullReferenceException("Source was empty");

        // Assert
        Assert.AreEqual(limit, responseJson.Results.Count);
    }
    
    [TestMethod]
    public async Task Limit0Returns20()
    {
        // Arrange
        const string specificUrl = Url + "?limit=0";
        
        // Act
        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<PokemonResponse>(response);

        if (responseJson is null || !responseJson.Results.Any())
            throw new NullReferenceException("Source was empty");

        // Assert
        Assert.AreEqual(20, responseJson.Results.Count);
    }
    
    [TestMethod]
    public async Task NoLimitReturns20()
    {
        // Arrange, Act
        var response = await _httpClient.GetStringAsync(Url);
        var responseJson = JsonConvert.DeserializeObject<PokemonResponse>(response);

        if (responseJson is null || !responseJson.Results.Any())
            throw new NullReferenceException("Source was empty");

        // Assert
        Assert.AreEqual(20, responseJson.Results.Count);
    }
    
    [TestMethod]
    [DataRow(6)]
    [DataRow(57)]
    public async Task OffsetWorksRight(int offset)
    {
        // Arrange
        var specificUrl = Url + $"?offset={offset}";
        
        // Act
        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<PokemonResponse>(response);

        if (responseJson is null || !responseJson.Results.Any())
            throw new NullReferenceException("Source was empty");

        // Assert
        // Если тестировать на большом offset, то может выдавать ошибку,
        // потому что между Id покемонов может быть промежуток
        // Например id=1000, сделующий может быть id=1005
        Assert.AreEqual(offset + 1, responseJson.Results.First().Id);
    }

    [TestMethod]
    [DataRow(4, 11)]
    public async Task LimitAndOffsetWorkRight(int limit, int offset)
    {
        // Arrange
        var specificUrl = Url + $"?limit={limit}" + $"&offset={offset}";
        
        // Act
        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<PokemonResponse>(response);

        if (responseJson is null || !responseJson.Results.Any())
            throw new NullReferenceException("Source was empty");

        // Assert
        Assert.IsTrue(offset + 1 == responseJson.Results.First().Id && responseJson.Results.Count == limit);
    }
}