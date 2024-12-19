using ErgastF1ApiServiceCache.Application.Interfaces;
using ErgastF1ApiServiceCache.Application.Services;
using ErgastF1ApiServiceCache.Domain.Interfaces;
using ErgastF1ApiServiceCache.Domain.Interfaces.Repositories;
using ErgastF1ApiServiceCache.Infrastructure.Cache;
using ErgastF1ApiServiceCache.Infrastructure.Repositories;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
