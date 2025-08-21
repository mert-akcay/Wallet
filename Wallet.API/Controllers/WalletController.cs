using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Commands;
using Wallet.Application.Inputs;
using Wallet.Application.Queries;

namespace Wallet.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalletController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(WalletCreateInput input)
    {
        var command = new WalletCreateCommand(input);
        var response = await mediator.Send(command);
        //TODO error handling
        return Ok(response);
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetById()
    {
        var userId = new Guid("fbcc29cb-5a05-4cbc-9a1e-04654ca0c000");
        var walletId = new Guid("eb24d14d-12b4-44b2-b92c-efd5aab7fac8");
        var input = new WalletByWalletIdInput()
        {
            UserId = userId,
            WalletId = walletId
        };
        var query = new WalletByWalletIdQuery(input);
        var response = await mediator.Send(query);
        //TODO error handling
        return Ok(response);
    }
}
