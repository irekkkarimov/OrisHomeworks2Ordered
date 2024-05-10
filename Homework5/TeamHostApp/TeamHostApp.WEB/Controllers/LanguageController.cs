using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace TeamHostApp.WEB.Controllers;

public class LanguageController : Controller
{
    [HttpGet]
    public IActionResult ChangeLanguage([FromQuery] string culture)
    {
        if (string.IsNullOrEmpty(culture) || string.IsNullOrWhiteSpace(culture))
            Redirect("/");

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

        return Redirect("/");
    }
}