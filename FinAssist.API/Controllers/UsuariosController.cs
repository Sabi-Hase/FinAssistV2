using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinAssist.API.Data;
using FinAssist.Shared.Models;

namespace FinAssist.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
    {
        return await _context.Usuarios
            .Include(u => u.Despesas)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetById(int id)
    {
        var user = await _context.Usuarios
            .Include(u => u.Despesas)
            .FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> Create([FromBody] Usuario user)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

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
