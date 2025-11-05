using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FinAssist.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar suporte a HttpClientFactory (necessário para o CambioController)
builder.Services.AddHttpClient();

// Configurar Controllers + opções JSON (para evitar loops de referência)
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FinAssist API",
        Version = "v1",
        Description = "API para gestão financeira pessoal, com cadastro de usuários, despesas, câmbio e análise de risco simulada.",
        Contact = new OpenApiContact
        {
            Name = "FinAssist - Equipe de Desenvolvimento",
            Email = "contato@finassist.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT License"
        }
    });

    // Incluir comentários XML (caso o arquivo tenha sido gerado pelo projeto)
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configurar middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinAssist v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
