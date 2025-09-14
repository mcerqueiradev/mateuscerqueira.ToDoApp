# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution files
COPY ["mateuscerqueira.ToDoApp.WebApi/mateuscerqueira.ToDoApp.WebApi.csproj", "WebApi/"]
COPY ["mateuscerqueira.Data/mateuscerqueira.Data.csproj", "Data/"]
COPY ["mateuscerqueira.Application/mateuscerqueira.Application.csproj", "Application/"]
COPY ["mateuscerqueira.ToDoApp.Domain/mateuscerqueira.ToDoApp.Domain.csproj", "Domain/"]
COPY ["mateuscerqueira.ToDoApp.Domain.Core/mateuscerqueira.ToDoApp.Domain.Core.csproj", "Domain.Core/"]

# Restore packages
RUN dotnet restore "WebApi/mateuscerqueira.ToDoApp.WebApi.csproj"

# Copy everything else
COPY . .

# Build the application
WORKDIR "/src/WebApi"
RUN dotnet build -c Release --no-restore

# Install EF Core tool
RUN dotnet tool install --global dotnet-ef --version 9.0.9

# Run database migrations
RUN dotnet ef database update --project ../mateuscerqueira.Data/mateuscerqueira.Data.csproj --no-build

# Publish the application
RUN dotnet publish -c Release -o /app/publish --no-restore --no-build

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Create app directory and set permissions
RUN mkdir -p /app && chmod 755 /app

# Copy published application
COPY --from=build /app/publish .

# Add dotnet tools to PATH
ENV PATH="${PATH}:/root/.dotnet/tools"

# Expose port
EXPOSE 8080

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:8080/api/health || exit 1

# Entry point
ENTRYPOINT ["dotnet", "mateuscerqueira.ToDoApp.WebApi.dll"]