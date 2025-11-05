using System.ComponentModel.DataAnnotations;

namespace FinAssist.Shared.Models
{
    public class Usuario
    {
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    // Coleção de despesas
    public List<Despesa> Despesas { get; set; } = new();
    }
}
