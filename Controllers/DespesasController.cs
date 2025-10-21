using FinAssistAPI.Data;
using FinAssistAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinAssistAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DespesasController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Despesa>>> GetDespesas() => await _context.Despesas.Include(d => d.Usuario).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Despesa>> GetDespesa(int id)
        {
            var despesa = await _context.Despesas.Include(d => d.Usuario).FirstOrDefaultAsync(d => d.Id == id);
            if (despesa == null) return NotFound();
            return despesa;
        }

        [HttpPost]
        public async Task<ActionResult<Despesa>> PostDespesa(Despesa despesa)
        {
            _context.Despesas.Add(despesa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDespesa), new { id = despesa.Id }, despesa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDespesa(int id, Despesa despesa)
        {
            if (id != despesa.Id) return BadRequest();
            _context.Entry(despesa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDespesa(int id)
        {
            var despesa = await _context.Despesas.FindAsync(id);
            if (despesa == null) return NotFound();
            _context.Despesas.Remove(despesa);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}