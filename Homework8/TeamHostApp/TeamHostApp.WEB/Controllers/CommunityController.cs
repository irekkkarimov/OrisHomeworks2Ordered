using Microsoft.AspNetCore.Mvc;

namespace TeamHostApp.WEB.Controllers;

public class CommunityController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}