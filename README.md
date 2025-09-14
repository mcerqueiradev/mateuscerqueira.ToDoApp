ToDoApp Backend - API .NET
🚀 Como rodar o Backend (API)

Pré-requisitos
```
.NET 9.0 SDK

PostgreSQL 15+ (ou Docker)

Git
```
1. Clone o repositório
```
bash
git clone https://github.com/mcerqueiradev/mateuscerqueira.ToDoApp.git
cd mateuscerqueira.ToDoApp/backend
```
2. Instale as dependências
```
bash
dotnet restore
dotnet build
```
3. Configure o Banco de Dados
Opção 1: PostgreSQL com Docker (Recomendado)
```
bash
docker run -d --name postgres \
  -e POSTGRES_DB=todoapp \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=password \
  -p 5432:5432 postgres:15
```
Opção 2: PostgreSQL Local

Instale PostgreSQL 15+
Crie database todoapp
Configure a Connection String

appsettings.Development.json:
```
json
{
  "ConnectionStrings": {
    "ToDoAppCS": "Server=localhost;Port=5432;Database=todoapp;User Id=postgres;Password=password;"
  },
  "JwtSettings": {
    "Key": "sua-chave-jwt-secreta-aqui-minimo-64-caracteres-abcdefghijklmnopqrstuvwxyz123",
    "Issuer": "ToDoApp",
    "Audience": "ToDoAppUsers",
    "ExpirationMinutes": 60
  }
}
```
4. Aplique as Migrações
```
bash
dotnet ef database update --project mateuscerqueira.Data --startup-project mateuscerqueira.ToDoApp.WebApi
```
5. Execute a API
```
bash
# Desenvolvimento
dotnet run --project mateuscerqueira.ToDoApp.WebApi

# Com hot reload
dotnet watch run --project mateuscerqueira.ToDoApp.WebApi
API disponível em: http://localhost:5000
Swagger UI: http://localhost:5000/swagger
```

🗄️ Configuração do Banco de Dados
Variáveis de Ambiente (Produção)
No Render ou outro hosting, configure:

```
bash
CONNECTIONSTRINGS__TODOCS=Server=host;Port=5432;Database=db;User Id=user;Password=pass;SSL Mode=Require;
ASPNETCORE_ENVIRONMENT=Production
JwtSettings__Key=sua-chave-jwt-secreta-minimo-64-caracteres-abcdefghijklmnopqrstuvwxyz123
JwtSettings__Issuer=ToDoApp
JwtSettings__Audience=ToDoAppUsers
JwtSettings__ExpirationMinutes=60
```

Migrações Automáticas
O projeto inclui migrações do Entity Framework Core para:
Tabela de Users com value objects
Tabela de ToDoItems
Sistema de auditoria
Configurações de segurança

⭐ Diferenciais Implementados
🏗️ Arquitetura Avançada
Clean Architecture com camadas bem definidas

Domain-Driven Design com entidades ricas

CQRS Pattern com MediatR

Repository Pattern com Unit of Work

🔐 Segurança Robusta
JWT Authentication com refresh tokens
Password hashing com PBKDF2
CORS configurado para produção e desenvolvimento

🚀 Performance & Boas Práticas
Async/Await em todas as operações I/O
Dependency Injection nativa
Docker-ready com multi-stage builds

📦 Estrutura do Projeto
```
text
backend/
├── mateuscerqueira.ToDoApp.Domain/          # Entidades e value objects
├── mateuscerqueira.ToDoApp.Domain.Core/     # Interfaces e contratos
├── mateuscerqueira.Application/             # Casos de uso e CQRS
├── mateuscerqueira.Data/                    # EF Core e repositórios
├── mateuscerqueira.ToDoApp.WebApi/          # Controllers e endpoints
└── mateuscerqueira.ToDoApp.Security/        # Autenticação e JWT
```

🎯 Endpoints Principais
```
Método	Endpoint	Descrição
POST	/api/Users	Criar usuário
POST	/api/Auth/login	Login
GET	/api/Users	Listar usuários
GET	/api/ToDoItems	Listar tarefas
POST	/api/ToDoItems	Criar tarefa
```

🚀 Deploy no Render
Conecte o repositório GitHub
Configure as variáveis de ambiente

Build Command:
```
bash
dotnet restore && dotnet publish -c Release -o ./publish
```
Start Command:
```
bash
dotnet ./publish/mateuscerqueira.ToDoApp.WebApi.dll
```

📞 Suporte
Desenvolvido por: Mateus Cerqueira
Email: mateusjesus2309@gmail.com
GitHub: mcerqueiradev

