using Microsoft.AspNetCore.Mvc;
using FinAssist.Shared.Models;

namespace FinAssist.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    // Este endpoint simula uma resposta de recomendação/triagem.
    // Para integrar com OpenAI, alterar o método para chamar a API real com a chave.

    [HttpPost("recommendation")]
    public IActionResult Recommendation([FromBody] dynamic payload)
    {
        // payload pode incluir histórico de despesas, valor médio gasto, etc.
        // Aqui retornamos uma resposta simples e segura para demo.
        var resp = new {
            message = "Com base nos dados fornecidos, detectamos padrões de risco moderado. Recomendamos reduzir apostas em 30% e procurar suporte profissional.",
            actions = new[] { "Agendar consulta", "Ativar bloqueio temporário de apostas", "Receber material educativo" }
        };
        return Ok(resp);
    }
}
