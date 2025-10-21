namespace FinAssistAPI.Models
{
    public class Despesa
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public Usuario? Usuario { get; set; }
    }
}