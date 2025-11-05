using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FinAssist.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CambioController : ControllerBase
{
    private readonly IHttpClientFactory _httpFactory;

    public CambioController(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    [HttpGet("usd-to-brl")]
    public async Task<IActionResult> GetUsdToBrl()
    {
        var client = _httpFactory.CreateClient();
        // Exemplo de endpoint público sem API key
        var url = "https://open.er-api.com/v6/latest/USD";
        var res = await client.GetAsync(url);
        if (!res.IsSuccessStatusCode) return StatusCode((int)res.StatusCode, "Erro ao consultar câmbio");
        var text = await res.Content.ReadAsStringAsync();
        var json = JObject.Parse(text);
        var rate = json["rates"]?["BRL"]?.Value<decimal>() ?? 0m;
        return Ok(new { baseCurrency = "USD", target = "BRL", rate });
    }
}
