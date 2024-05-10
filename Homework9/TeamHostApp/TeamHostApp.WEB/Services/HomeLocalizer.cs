using Microsoft.Extensions.Localization;
using TeamHostApp.WEB.Controllers;

namespace TeamHostApp.WEB.Services;

public class HomeLocalizer : IStringLocalizer<HomeController>
{
    private readonly IDictionary<(string, string), LocalizedString> _dictionary = new Dictionary<(string, string), LocalizedString>();

    public HomeLocalizer()
    {
        var newString = new LocalizedString("Title", "Privet", false, "ru");
        _dictionary.Add((newString.Name, newString.SearchedLocation)!, newString);
    }
    
    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        return _dictionary.Values;
    }

    public LocalizedString this[string name] => _dictionary[(name, "ru")];

    public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();
}