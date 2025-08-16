using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Commands;
using Wallet.Application.Inputs;

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
}
