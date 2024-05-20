using NEventStore;
using NEventStore.Persistence.Sql.SqlDialects;
using NEventStore.Serialization.Json;
using Npgsql;
using System.Data.Common;
using Wallet.API.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<WalletDomainRepository>();
DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);

builder.Services.AddScoped<IStoreEvents>(sp =>
{
    var loggerFactory = LoggerFactory.Create(logging =>
    {
        logging
            .AddConsole()
            .AddDebug()
            .SetMinimumLevel(LogLevel.Trace);
    });
    return Wireup.Init()
        .WithLoggerFactory(loggerFactory)
        .UsingSqlPersistence(DbProviderFactories.GetFactory("Npgsql"), builder.Configuration["EventStore:ConnectionString"])
        .WithDialect(new PostgreSqlDialect())
        .InitializeStorageEngine()
        .UsingJsonSerialization()
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
