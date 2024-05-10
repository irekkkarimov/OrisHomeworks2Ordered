using Microsoft.AspNetCore.Mvc;

namespace TeamHostApp.WEB.Controllers;

public class FriendsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}