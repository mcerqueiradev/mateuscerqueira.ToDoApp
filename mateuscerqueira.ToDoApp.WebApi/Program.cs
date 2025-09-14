using mateuscerqueira.ToDoApp.IoC;

var builder = WebApplication.CreateBuilder(args);

// Porta do Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

// Configuração de CORS
builder.Services.AddCors(options =>
{
    if (builder.Environment.IsProduction())
    {
        options.AddPolicy("AllowProduction",
            policy =>
            {
                policy.WithOrigins("https://seu-frontend.vercel.app")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
    }
    else
    {
        options.AddPolicy("AllowDev",
            policy =>
            {
                policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
    }
});

// Injeção de dependências
builder.Services.AddIocServices(builder.Configuration);

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aplica a política de CORS conforme ambiente
if (app.Environment.IsProduction())
{
    app.UseCors("AllowProduction");
}
else
{
    app.UseCors("AllowDev");
}

app.UseAuthorization();

app.MapControllers();

app.Run();
