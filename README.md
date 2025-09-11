ToDo App - Sistema de Gerenciamento de Tarefas
Uma aplicação  de gerenciamento de tarefas desenvolvida em .NET com arquitetura limpa e boas práticas de desenvolvimento.

Funcionalidades
Cadastro e autenticação de usuários

Criação e organização de tarefas

Categorização e priorização de atividades

Sistema de autenticação e autorização JWT

Dashboard com overview de produtividade

Interface responsiva e intuitiva

Tecnologias
Backend: .NET 8, Entity Framework Core, C#

Autenticação: JWT, ASP.NET Core Identity

Banco de Dados: SQL Server (com suporte a outros providers)

Arquitetura: Clean Architecture, Domain-Driven Design


Estrutura do Projeto
```
ToDoApp/
├── Domain/          # Entidades e regras de negócio
├── Application/     # Casos de uso e serviços
├── Infrastructure/  # Implementações concretas
├── WebAPI/         # Controladores e endpoints
└── Tests/          # Testes unitários e de integração
```

Como Executar

```
# Clone o repositório
git clone https://github.com/seu-usuario/todoapp.git

# Configure o banco de dados
dotnet ef database update

# Execute a aplicação
dotnet run --project WebAPI
```

Licença
Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

Contato
Mateus Cerqueira
Email: mateusjesus2309@gmail.com
