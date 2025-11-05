using Microsoft.AspNetCore.Mvc;

namespace FinAssist.API.Controllers;

/// <summary>
/// Controlador responsável por interações de recomendação e análise de risco (simulação).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    /// <summary>
    /// Simula uma recomendação de comportamento financeiro com base em dados enviados.
    /// Placeholder - integraria com Azure OpenAI para análise real.
    /// </summary>
    [HttpPost("recommendation")]
    public IActionResult Recommendation([FromBody] dynamic payload)
    {
        var resp = new
        {
            message = "Com base nos dados fornecidos, detectamos padrões de risco moderado. Recomendamos reduzir gastos e buscar orientação profissional.",
            actions = new[] { "Agendar consulta", "Ativar bloqueio temporário", "Receber material educativo" }
        };
        return Ok(resp);
    }
}
