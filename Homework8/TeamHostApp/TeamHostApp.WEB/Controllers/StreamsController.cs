using Microsoft.AspNetCore.Mvc;

namespace TeamHostApp.WEB.Controllers;

public class StreamsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}