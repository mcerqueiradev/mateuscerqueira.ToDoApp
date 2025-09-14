ToDoApp - Sistema de Gerenciamento de Tarefas
🚀 Como rodar o Backend (API)

Pré-requisitos
```
.NET 9.0 SDK

PostgreSQL 15+

Docker (opcional)
```

1. Clone o repositório
```
bash
git clone https://github.com/mcerqueiradev/mateuscerqueira.ToDoApp.git
cd mateuscerqueira.ToDoApp/backend
```

2. Configure o ambiente
```
bash
# Instale as dependências
dotnet restore

# Build do projeto
dotnet build
```

3. Execute a API
```
bash
# Desenvolvimento
dotnet run --project mateuscerqueira.ToDoApp.WebApi
```

# Ou com watch
```dotnet watch run --project mateuscerqueira.ToDoApp.WebApi```
API disponível em: http://localhost:5000
