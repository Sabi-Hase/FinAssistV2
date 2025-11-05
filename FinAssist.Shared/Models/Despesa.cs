using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinAssist.Shared.Models
{
    public class Despesa
    {
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public decimal Valor { get; set; }

    [Required]
    public DateTime Data { get; set; } = DateTime.UtcNow;

    // FK
    public int UsuarioId { get; set; }

    // Navegação - não causará loop por causa do ReferenceHandler.IgnoreCycles
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Usuario? Usuario { get; set; }
    }
}
