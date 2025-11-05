    # FinAssist.API - Versão Reformulada

    Projeto de exemplo: API Web em ASP.NET Core para gerenciamento de usuários e despesas,
    com exemplos de consultas LINQ, integração com API pública (câmbio) e endpoint de recomendação.

    ## Como rodar localmente

    Requisitos:
- .NET 8 SDK
- SQL Server LocalDB (Windows) ou outra instância SQL Server

Passos:
1. Abra um terminal na pasta FinAssist.API
2. Restaure pacotes: `dotnet restore`
3. Crie migração e atualize banco:
   - `dotnet ef migrations add InitialCreate`
   - `dotnet ef database update`
4. Rode a aplicação: `dotnet run`
5. Abra `https://localhost:5001/swagger` para ver a documentação Swagger.

## Notas
- Substitua a connection string em `appsettings.json` conforme seu ambiente.
- Para integrar com OpenAI, adicione a chamada HTTP no ChatController e configure a chave em secrets/variáveis de ambiente.
