using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using FinAssist.Shared.Models;

namespace FinAssist.Web.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Usuario>>("http://localhost:5000/api/Usuarios");
        }

        public async Task<List<Despesa>> GetDespesasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Despesa>>("http://localhost:5000/api/Despesas");
        }
    }
}
