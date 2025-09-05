using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CardService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardController : ControllerBase
{

    [HttpPost("new-card")]
    public IActionResult GenerateCardNumber(Guid walletId)
    {
        string cardNumber;

        cardNumber = GenerateRandomCardNumber();

        return Ok(new { CardNumber = cardNumber });
    }

    private string GenerateRandomCardNumber()
    {
        var random = new Random();
        var builder = new StringBuilder();

        for (int i = 0; i < 4; i++)
        {
            if (i > 0) builder.Append("-");
            builder.Append(random.Next(1000, 9999));
        }

        return builder.ToString();
    }
}
