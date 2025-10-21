  - 98215 - Gabriel Antony Cadima Ciziks
  - 98169 - Lucca Sabatini Tambellini
  - 550781 - Sabrina Flores Morais
  - 551059 - Cassio Valezzi

# FinAssist (Console App)

FinAssist é um aplicativo console em C# desenvolvido para auxiliar pessoas com vício em apostas e gastos impulsivos.

Atualmente, o programa possui funcionalidades básicas de cadastro de usuários, geração de relatórios simples e sugestões de atividades saudáveis.

---

## Funcionalidades Atuais

### 1. Cadastrar Usuário

* Permite registrar o nome e uma estimativa de gastos do usuário.
* Os dados são salvos no banco **SQLite**.

### 2. Listar Usuários

* Exibe todos os usuários cadastrados no banco.
* Mostra o `Id`, `Nome` e `Gastos`.

### 3. Gerar Relatório

* Cria um arquivo `relatorio.txt` com a data de geração.
* Ainda não contém análise detalhada do comportamento de apostas.

### 4. Sugestões de Atividades

* Fornece uma lista fixa de alternativas saudáveis para desviar a atenção de apostas:

  * Caminhada
  * Meditação
  * Leitura
  * Curso online

### 5. Sair

* Encerra o programa.

---

## Pré-requisitos

* [.NET 9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
* SQLite
* Visual Studio Code ou outro editor compatível

---

## Como Executar

1. Abra o terminal na pasta do projeto:

```bash
cd src/FinAssist
```

2. Restaurar pacotes e compilar:

```bash
dotnet restore
dotnet build
```

3. Executar o aplicativo:

```bash
dotnet run
```

---

## Estrutura do Projeto

* `Program.cs` - ponto de entrada do console.
* `Models/` - classes de dados, como `Usuario`.
* `Services/` - lógica de negócio, CRUD e menu.
* `Data/` - configuração do banco SQLite.
* `README.md` - documentação do projeto.

---

## Licença

Este projeto está licenciado sob a **MIT License**.
