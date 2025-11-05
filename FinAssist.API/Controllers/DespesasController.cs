using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinAssist.API.Data;
using FinAssist.Shared.Models;

namespace FinAssist.API.Controllers
{
    /// <summary>
    /// Controlador responsável pelo gerenciamento das despesas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DespesasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DespesasController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todas as despesas cadastradas, incluindo os dados do usuário relacionado.
        /// </summary>
        /// <returns>Lista de despesas com informações do usuário.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Despesa>>> GetAll()
        {
            return await _context.Despesas
                .Include(d => d.Usuario)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna uma despesa específica pelo seu ID.
        /// </summary>
        /// <param name="id">ID da despesa desejada.</param>
        /// <returns>Objeto despesa correspondente, se encontrado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Despesa>> GetById(int id)
        {
            var despesa = await _context.Despesas
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (despesa == null)
                return NotFound();

            return Ok(despesa);
        }

        /// <summary>
        /// Retorna todas as despesas de um usuário específico.
        /// </summary>
        /// <param name="usuarioId">ID do usuário.</param>
        /// <returns>Lista de despesas associadas ao usuário informado.</returns>
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Despesa>>> GetByUsuario(int usuarioId)
        {
            var list = await _context.Despesas
                .Where(d => d.UsuarioId == usuarioId)
                .Include(d => d.Usuario)
                .OrderByDescending(d => d.Data)
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>
        /// Retorna um resumo das despesas agrupadas por usuário,
        /// mostrando o total gasto e a quantidade de despesas registradas.
        /// </summary>
        /// <returns>Lista com total e quantidade de despesas por usuário.</returns>
        [HttpGet("resumo/por-usuario")]
        public async Task<ActionResult> ResumoPorUsuario()
        {
            var resumo = await _context.Despesas
                .GroupBy(d => d.UsuarioId)
                .Select(g => new
                {
                    UsuarioId = g.Key,
                    Total = g.Sum(x => x.Valor),
                    Qtd = g.Count()
                })
                .ToListAsync();

            return Ok(resumo);
        }

        /// <summary>
        /// Cadastra uma nova despesa.
        /// </summary>
        /// <param name="despesa">Objeto despesa a ser criado.</param>
        /// <returns>Despesa criada, com ID atribuído.</returns>
        [HttpPost]
        public async Task<ActionResult<Despesa>> Create([FromBody] Despesa despesa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verifica se o usuário informado existe
            var userExists = await _context.Usuarios.AnyAsync(u => u.Id == despesa.UsuarioId);
            if (!userExists)
                return BadRequest(new { message = "UsuarioId inválido" });

            _context.Despesas.Add(despesa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = despesa.Id }, despesa);
        }

        /// <summary>
        /// Atualiza uma despesa existente.
        /// </summary>
        /// <param name="id">ID da despesa a ser atualizada.</param>
        /// <param name="despesa">Objeto despesa com os novos dados.</param>
        /// <returns>Status da operação.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Despesa despesa)
        {
            if (id != despesa.Id)
                return BadRequest(new { message = "O ID informado não corresponde à despesa." });

            var exists = await _context.Despesas.AnyAsync(d => d.Id == id);
            if (!exists)
                return NotFound();

            _context.Entry(despesa).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Exclui uma despesa pelo seu ID.
        /// </summary>
        /// <param name="id">ID da despesa a ser excluída.</param>
        /// <returns>Status da exclusão.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Despesas.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.Despesas.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
