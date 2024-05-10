using Microsoft.AspNetCore.Mvc;

namespace TeamHostApp.WEB.Controllers;

public class WalletController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}