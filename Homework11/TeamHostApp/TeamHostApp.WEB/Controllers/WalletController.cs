using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamHost.Application.DTOs.GamePurchase;
using TeamHost.Application.DTOs.Wallet;
using TeamHost.Application.Features.Wallet.Commands;
using TeamHost.Application.Features.Wallet.Queries;
using TeamHost.Domain.Entities.WalletEntities;

namespace TeamHostApp.WEB.Controllers;

[Route("[controller]")]
public class WalletController : Controller
{
    private readonly IMediator _mediator;

    public WalletController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var query = new GetWalletInfoQuery();
        var walletInfo = await _mediator.Send(query);
        
        return View(walletInfo);
    }

    [HttpPost]
    public async Task<IActionResult> PurchaseGame([FromQuery] int gameId)
    {
        var command = new BuyGameCommand(new MakePurchaseDto
        {
            GameId = gameId
        });
        await _mediator.Send(command);

        return Ok();
    }

    [HttpPatch]
    [Route("Deposit")]
    public async Task<IActionResult> Deposit([FromBody] DepositWalletRequest request)
    {
        var command = new DepositWalletCommand(request);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPatch]
    [Route("Withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] WithdrawWalletRequest request)
    {
        var command = new WithdrawWalletCommand(request);
        await _mediator.Send(command);
        return Ok();
    }
}