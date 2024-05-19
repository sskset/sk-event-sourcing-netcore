using NEventStore;
using SK.EventSourcing;
using Wallet.API.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<WalletDomainRepository>();

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
        .UsingInMemoryPersistence()
        .InitializeStorageEngine()
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
