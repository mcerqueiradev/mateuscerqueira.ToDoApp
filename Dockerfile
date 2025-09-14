# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy ALL .csproj files first (isso cria camadas em cache)
COPY *.sln .
COPY mateuscerqueira.ToDoApp.WebApi/*.csproj ./mateuscerqueira.ToDoApp.WebApi/
COPY mateuscerqueira.Data/*.csproj ./mateuscerqueira.Data/
COPY mateuscerqueira.Application/*.csproj ./mateuscerqueira.Application/
COPY mateuscerqueira.ToDoApp.Domain/*.csproj ./mateuscerqueira.ToDoApp.Domain/
COPY mateuscerqueira.ToDoApp.Domain.Core/*.csproj ./mateuscerqueira.ToDoApp.Domain.Core/
COPY mateuscerqueira.ToDoApp.IoC/*.csproj ./mateuscerqueira.ToDoApp.IoC/
COPY mateuscerqueira.ToDoApp.Security/*.csproj ./mateuscerqueira.ToDoApp.Security/

# Restore packages for the solution
RUN dotnet restore

# Copy ALL source code
COPY . .

# Build and publish
WORKDIR /src/mateuscerqueira.ToDoApp.WebApi
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "mateuscerqueira.ToDoApp.WebApi.dll"]