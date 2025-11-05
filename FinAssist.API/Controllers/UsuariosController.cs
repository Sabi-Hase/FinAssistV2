using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinAssist.API.Data;
using FinAssist.Shared.Models;

namespace FinAssist.API.Controllers;

/// <summary>
/// Controlador responsável pelo gerenciamento de usuários.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna todos os usuários cadastrados, incluindo suas despesas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
    {
        return await _context.Usuarios
            .Include(u => u.Despesas)
            .ToListAsync();
    }

    /// <summary>
    /// Retorna um usuário específico pelo seu ID.
    /// </summary>
    /// <param name="id">ID do usuário.</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetById(int id)
    {
        var user = await _context.Usuarios
            .Include(u => u.Despesas)
            .FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    /// <summary>
    /// Cadastra um novo usuário.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Usuario>> Create([FromBody] Usuario user)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    /// <summary>
    /// Atualiza os dados de um usuário existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Usuario user)
    {
        if (id != user.Id) return BadRequest();
        var exists = await _context.Usuarios.AnyAsync(u => u.Id == id);
        if (!exists) return NotFound();
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// Exclui um usuário existente.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _context.Usuarios.FindAsync(id);
        if (user == null) return NotFound();
        _context.Usuarios.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
