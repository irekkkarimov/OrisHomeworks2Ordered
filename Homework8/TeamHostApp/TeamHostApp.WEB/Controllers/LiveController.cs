using Microsoft.AspNetCore.Mvc;

namespace TeamHostApp.WEB.Controllers;

public class LiveController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public Task<IActionResult> SendStreamUrl(string url)
    {
        return Task.FromResult<IActionResult>(Ok());
    }
}