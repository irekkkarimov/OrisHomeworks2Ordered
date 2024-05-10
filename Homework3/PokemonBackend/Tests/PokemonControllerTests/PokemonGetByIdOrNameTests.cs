using System.Net;
using Newtonsoft.Json;
using PokemonAPI.Models;

namespace Tests.PokemonControllerTests;

[TestClass]
public class PokemonGetByIdOrNameTests
{
    private readonly HttpClient _httpClient = new();
    private const string Url = "http://localhost:5254/pokemon/GetByIdOrName";

    [TestMethod]
    [DataRow(1)]
    [DataRow(456)]
    public async Task GetByIdWorksRight(int id)
    {
        // Arrange
        var specificUrl = Url + $"/{id}";
        
        // Act
        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<PokemonDetailed>(response);
        
        // Assert
        Assert.IsTrue(responseJson is not null && responseJson.Name.Any());
    }
    
    [TestMethod]
    [DataRow("bulbasaur")]
    [DataRow("PIKACHU")]
    public async Task GetByNameWorksRight(string name)
    {
        // Arrange
        var specificUrl = Url + $"/{name}";
        
        // Act
        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<PokemonDetailed>(response);
        
        // Assert
        Assert.IsTrue(responseJson is not null && responseJson.Name.Any());
    }

    [TestMethod]
    [DataRow("0")]
    [DataRow("notFound")]
    public async Task WrongNameOrIdReturnsNotFound(string idOrName)
    {
        // Arrange
        var specificUrl = Url + $"/{idOrName}";
        
        // Act
        var response = await _httpClient.GetAsync(specificUrl);
        
        // Assert
        Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
    }
}