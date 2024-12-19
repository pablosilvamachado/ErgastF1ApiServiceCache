using ErgastF1ApiServiceCache.Application.Interfaces;
using ErgastF1ApiServiceCache.Application.Services;
using ErgastF1ApiServiceCache.Domain.Interfaces;
using ErgastF1ApiServiceCache.Domain.Interfaces.Repositories;
using ErgastF1ApiServiceCache.Infrastructure.Cache;
using ErgastF1ApiServiceCache.Infrastructure.Repositories;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Configuração do Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
{
    var configuration = builder.Configuration.GetValue<string>("Redis:ConnectionString");
    return ConnectionMultiplexer.Connect(configuration);
});

// HttpClient
builder.Services.AddHttpClient();

// Serviços
builder.Services.AddSingleton<ICacheService, RedisCacheService>();
builder.Services.AddScoped<ISeasonService, SeasonService>();
builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Formula 1 API",
        Version = "v1",
        Description = "API para obter dados relacionados à Fórmula 1, atualmente suportando Seasons.",
        Contact = new OpenApiContact
        {
            Name = "Pablo da Silva mMachado",
            Email = "pablosilvamachado@hotmail.com",
            Url = new Uri("https://github.com/pablosilvamachado")
        }
    });
});


string[] origens = ["http://localhost:4200", "https://localhost:4200", "http://localhost:8080", "http://localhost:8081", "http://localhost:5248"];

builder.Services.AddCors(
                options => options.AddPolicy(
                    MyAllowSpecificOrigins,
                    builder => builder
                        .WithOrigins(origens)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );

var app = builder.Build();


// Configura o middleware do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Formula 1 API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
