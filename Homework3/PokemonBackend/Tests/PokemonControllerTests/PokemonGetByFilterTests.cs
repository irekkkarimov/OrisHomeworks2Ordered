using Newtonsoft.Json;
using PokemonAPI.Models.DTOs.ResponseDTOs;

namespace Tests.PokemonControllerTests;

[TestClass]
public class PokemonGetByFilterTests
{
    private readonly HttpClient _httpClient = new();
    private const string Url = "http://localhost:5254/pokemon/getbyfilter";
    
    [TestMethod]
    [DataRow("arch")]
    [DataRow("bulba")]
    public async Task AllPokemonNamesContainFilterString(string filter)
    {
        // Arrange
        var specificUrl = Url + $"/{filter}";

        // Act
        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<List<PokemonResponseItem>>(response);

        if (responseJson is null)
            throw new NullReferenceException("Source was empty");
        
        // Assert
        var ifAllPokemonNamesContainFilterString = responseJson
            .All(i => CheckIfPokemonNameContainFilterString(i, filter));
        Assert.IsTrue(ifAllPokemonNamesContainFilterString);
    }
    
    [TestMethod]
    [DataRow("Wrong_filter")]
    [DataRow("AlsoVeryWrongFilter")]
    public async Task WrongFilterReturnsEmptyList(string filter)
    {
        // Arrange
        var specificUrl = Url + $"/{filter}";

        // Act
        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<List<PokemonResponseItem>>(response);

        if (responseJson is null)
            throw new NullReferenceException("Source was empty");
        
        // Assert
        Assert.AreEqual(0, responseJson.Count);
    }
    
    [TestMethod]
    [DataRow("arch", "ARCH")]
    [DataRow("bulba", "BuLbA")]
    public async Task FilterIsNotCaseSensitive(string lowercase, string someCase)
    {
        // Arrange
        var specificUrl = Url + $"/{lowercase}";

        // Act 1
        var firstResponse = await _httpClient.GetStringAsync(specificUrl);
        var firstResponseJson = JsonConvert.DeserializeObject<List<PokemonResponseItem>>(firstResponse);
        
        // Act 2
        var secondResponse = await _httpClient.GetStringAsync(specificUrl);
        var secondResponseJson = JsonConvert.DeserializeObject<List<PokemonResponseItem>>(secondResponse);

        if (firstResponseJson is null || secondResponseJson is null)
            throw new NullReferenceException("Source was empty");
        
        // Assert
        Assert.AreEqual(firstResponseJson.Count, secondResponseJson.Count);
    }

    private static bool CheckIfPokemonNameContainFilterString(PokemonResponseItem pokemonResponseItem, string filter)
    {
        return pokemonResponseItem.Name.ToLower().Contains(filter.ToLower());
    }
}