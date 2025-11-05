using Microsoft.AspNetCore.Mvc.RazorPages;
using FinAssist.Shared.Models;
using FinAssist.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinAssist.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public List<Usuario> Usuarios { get; set; } = new();
        public List<Despesa> Despesas { get; set; } = new();

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task OnGetAsync()
        {
            Usuarios = await _apiService.GetUsuariosAsync();
            Despesas = await _apiService.GetDespesasAsync();
        }
    }
}
