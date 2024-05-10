using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamHost.Application.Features.Games.Queries;

namespace TeamHostApp.WEB.Controllers;

public class StoreController : Controller
{
    private readonly IMediator _mediator;

    public StoreController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var getAllGamesQuery = new GetAllGamesQuery();
        var allGames = await _mediator.Send(getAllGamesQuery);
        return View(allGames);
    }
}