using Microsoft.AspNetCore.Mvc;

namespace TeamHostApp.WEB.Controllers;

public class MarketController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}