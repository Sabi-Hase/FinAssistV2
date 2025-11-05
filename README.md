FinAssist API

Descrição
FinAssist é uma API desenvolvida em .NET 9 para gestão financeira pessoal, oferecendo recursos de:
- Cadastro e controle de usuários e despesas;
- Consulta de câmbio em tempo real (USD → BRL);
- Simulação de recomendações automáticas de triagem e risco financeiro (ChatController);
- Documentação automática via Swagger UI.

Este projeto foi desenvolvido com foco em boas práticas de arquitetura e integração com serviços externos.
Inclui endpoints RESTful, uso de Entity Framework Core e exemplos práticos de consumo de API externa.

----------------------------------------
Estrutura do Projeto

FinAssistV2/
│
├── FinAssist.API/              # Projeto principal (API REST)
│   ├── Controllers/            # Controladores da aplicação
│   │   ├── UsuariosController.cs
│   │   ├── DespesasController.cs
│   │   ├── CambioController.cs
│   │   └── ChatController.cs
│   ├── Data/                   # Contexto do banco (Entity Framework)
│   ├── appsettings.json        # Configurações de conexão e ambiente
│   └── Program.cs              # Configuração inicial da API
│
└── FinAssist.Shared/           # Modelos de dados (entidades compartilhadas)

----------------------------------------
Principais Funcionalidades

Usuarios
- GET /api/Usuarios — Lista todos os usuários cadastrados
- POST /api/Usuarios — Cria um novo usuário
- GET /api/Usuarios/{id} — Retorna um usuário específico
- PUT /api/Usuarios/{id} — Atualiza um usuário existente
- DELETE /api/Usuarios/{id} — Remove um usuário

Despesas
- GET /api/Despesas — Lista todas as despesas
- POST /api/Despesas — Cadastra uma nova despesa
- GET /api/Despesas/{id} — Consulta uma despesa específica
- GET /api/Despesas/resumo/por-usuario — Mostra total de gastos por usuário

Câmbio
- GET /api/Cambio/usd-to-brl — Consulta o valor atual do dólar em reais (via API pública)

Chat / Recomendação
- POST /api/Chat/recommendation — Retorna uma resposta simulada com recomendações de triagem financeira
  - Placeholder – integração futura com Azure/OpenAI para análise real de padrões de risco.

----------------------------------------
Dependências Principais

O projeto utiliza os seguintes pacotes NuGet:

Pacote                                      Descrição
Microsoft.EntityFrameworkCore               ORM para mapeamento e persistência dos dados
Microsoft.EntityFrameworkCore.SqlServer     Suporte a banco SQL Server
Microsoft.EntityFrameworkCore.Tools         Ferramentas para migrações
Swashbuckle.AspNetCore                      Geração automática de documentação Swagger
Newtonsoft.Json                             Manipulação avançada de JSON
Microsoft.AspNetCore.Http.Json              Serialização e opções de JSON nativas
System.Net.Http                             Utilizado para consumo de APIs externas

----------------------------------------
Requisitos para Execução

Antes de executar o projeto, verifique se possui instalado:
- .NET SDK 9.0 ou superior
- SQL Server (ou Azure SQL)
- Git (para clonar o repositório)
- Visual Studio / VS Code (opcional, mas recomendado)

----------------------------------------
Como Executar Localmente

1. Clonar o repositório
   git clone https://github.com/<seu-usuario>/FinAssistV2.git
   cd FinAssistV2/FinAssist.API

2. Configurar a string de conexão
   Editar o arquivo appsettings.json:
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=FinAssistDB;Trusted_Connection=True;TrustServerCertificate=True;"
   }

3. Criar o banco de dados
   dotnet ef database update

4. Executar a API
   dotnet run

   O terminal exibirá algo como:
   Now listening on: https://localhost:7010

5. Acessar a documentação Swagger
   Acesse: https://localhost:7010/swagger
   (Importante: o /swagger precisa ser adicionado manualmente ao link para abrir a interface interativa.)

----------------------------------------
Integrações Externas

- Câmbio (CambioController): Consome API pública https://open.er-api.com/v6/latest/USD
- ChatController (Placeholder): Futura integração com Azure OpenAI para análise de comportamento financeiro e recomendações automáticas.

----------------------------------------
Integrantes

- 98215 - Gabriel Antony Cadima Ciziks
- 98169 - Lucca Sabatini Tambellini
- 550781 - Sabrina Flores Morais
- 551059 - Cassio Valezzi

----------------------------------------
Licença

Distribuído sob a MIT License.
