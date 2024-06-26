using Domain.Model;
using Elastic.Clients.Elasticsearch;
using Serilog;
using Serilog.Events;
using TileProxyServer;
using TileProxyServer.Services;

var builder = WebApplication.CreateBuilder(args);

using var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {ClientIp}] {Message:lj}{NewLine}{Exception}")
    .Enrich.WithClientIp()
    .CreateLogger();

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ProxyConfiguration>(builder.Configuration.GetSection("ProxyConfiguration"));
var elasticSettings = new ElasticsearchClientSettings().DefaultIndex("tile-server-index");
var elasticSearchClient = new ElasticsearchClient(elasticSettings);

await elasticSearchClient.DeleteByQueryAsync<Request>(
    s => s.Query(q => q.QueryString(qs => qs.Query("*")))
);

builder.Services.AddSingleton(elasticSearchClient);
builder.Services.AddSingleton<IIpAddressVerificationService, IpAddressVerificationService>();


var sqliteConnectionString = builder.Configuration.GetSection("ProxyConfiguration:SqliteConnectionString").Value ?? string.Empty;
builder.Services.AddSingleton<IBlacklistDatabaseService>(_ => new BlacklistDatabaseService(sqliteConnectionString));

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
