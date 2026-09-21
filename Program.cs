using CryptoTrackerApi.Application.DTOs;
using CryptoTrackerApi.Application.Interfaces;
using CryptoTrackerApi.Application.UseCases.Blockchain.Commands;
using CryptoTrackerApi.Application.UseCases.Blockchain.Queries;
using CryptoTrackerApi.Infrastructure.ExternalServices;
using CryptoTrackerApi.Infrastructure.Persistence;
using CryptoTrackerApi.Infrastructure.Swagger;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Bind BlockCypher options
builder.Services.Configure<BlockCypherSettings>(
    builder.Configuration.GetSection(BlockCypherSettings.SectionName));

// Add HttpClient for external API calls
builder.Services.AddHttpClient();

// Add BlockCypher clients for UTXO and Account blockchain data
builder.Services.AddTransient<BlockCypherClient<UtxoBlockchainDataDto>>();
builder.Services.AddTransient<BlockCypherClient<AccountBlockchainDataDto>>();

// Add orchestrator to manage fetching and mapping data from BlockCypher 
builder.Services.AddScoped<IBlockCypherOrchestrator, BlockCypherOrchestrator>();

// Add services to the container
// Add controllers with JSON options to handle enum serialization as strings
builder.Services.AddControllers()
    .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<EnumSchemaFilter>();
});

// Database
var connectionFromEnv = Environment.GetEnvironmentVariable("CONNECTION_STRING");
var connectionString = connectionFromEnv ?? builder.Configuration.GetConnectionString("DefaultConnection");

// Repositories
builder.Services.AddScoped<IBlockchainRepository, BlockchainRepository>();

if (string.IsNullOrWhiteSpace(connectionString) || connectionString.Trim().Equals("Data Source=crypto.db", StringComparison.OrdinalIgnoreCase))
{
    var dataFolder = Path.Combine(builder.Environment.ContentRootPath, "Data");
    Directory.CreateDirectory(dataFolder);
    var dbPath = Path.Combine(dataFolder, "crypto.db");
    connectionString = $"Data Source={dbPath}";
}

builder.Services.AddDbContext<CryptoDbContext>(options =>
    options.UseSqlite(connectionString));

// AutoMapper
builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

// Handlers (CQRS)
builder.Services.AddScoped<FetchAndStoreBlockchainCommandHandler>();
builder.Services.AddScoped<GetBlockchainHistoryQueryHandler>();
builder.Services.AddScoped<GetBlockchainHistoryByNetworkQueryHandler>();

var app = builder.Build();

// Ensure DB and tables exist (create on first run)
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        logger.LogInformation("Ensuring database is created...");
        var dbContext = scope.ServiceProvider.GetRequiredService<CryptoDbContext>();
        dbContext.Database.EnsureCreated();

        dbContext.Database.ExecuteSqlRaw("PRAGMA journal_mode=WAL;");

        logger.LogInformation("Database ensured/created.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error while ensuring database creation.");
        throw;
    }
}

// Configure the HTTP request pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Functional Testing
public partial class Program { }