using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace FinAssistAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CambioController : ControllerBase
    {
        private readonly HttpClient _client;
        public CambioController(IHttpClientFactory httpClientFactory) => _client = httpClientFactory.CreateClient();

        [HttpGet]
        public async Task<ActionResult<decimal>> GetCambio()
        {
            var response = await _client.GetStringAsync("https://api.exchangerate.host/latest?base=USD&symbols=BRL");
            var json = JObject.Parse(response);
            decimal valor = json["rates"]?["BRL"]?.Value<decimal>() ?? 0;
            return Ok(valor);
        }
    }
}