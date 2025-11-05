using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinAssist.API.Data;
using FinAssist.Shared.Models;

namespace FinAssist.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DespesasController : ControllerBase
{
    private readonly AppDbContext _context;

    public DespesasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Despesa>>> GetAll()
    {
        return await _context.Despesas
            .Include(d => d.Usuario)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Despesa>> GetById(int id)
    {
        var d = await _context.Despesas
            .Include(x => x.Usuario)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (d == null) return NotFound();
        return Ok(d);
    }

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

    // LINQ não trivial: resumo por usuário (soma de despesas)
    [HttpGet("resumo/por-usuario")]
    public async Task<ActionResult> ResumoPorUsuario()
    {
        var resumo = await _context.Despesas
            .GroupBy(d => d.UsuarioId)
            .Select(g => new {
                UsuarioId = g.Key,
                Total = g.Sum(x => x.Valor),
                Qtd = g.Count()
            }).ToListAsync();
        return Ok(resumo);
    }

    [HttpPost]
    public async Task<ActionResult<Despesa>> Create([FromBody] Despesa despesa)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        // assegura que o usuário referenciado exista
        var userExists = await _context.Usuarios.AnyAsync(u => u.Id == despesa.UsuarioId);
        if (!userExists) return BadRequest(new { message = "UsuarioId inválido" });

        _context.Despesas.Add(despesa);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = despesa.Id }, despesa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Despesa desp)
    {
        if (id != desp.Id) return BadRequest();
        var exists = await _context.Despesas.AnyAsync(d => d.Id == id);
        if (!exists) return NotFound();
        _context.Entry(desp).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Despesas.FindAsync(id);
        if (entity == null) return NotFound();
        _context.Despesas.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
