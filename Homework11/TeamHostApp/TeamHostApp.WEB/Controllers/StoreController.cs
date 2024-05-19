using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamHost.Application.DTOs.Game;
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
    public async Task<IActionResult> Index([FromQuery] string? filter = null)
    {
        var getGamesByFilterQuery = new GetGamesByFilterQuery(new GetGamesByFilterRequest
        {
            Filter = filter
        });
        var allGames = await _mediator.Send(getGamesByFilterQuery);
        return View(allGames);
    }
}