using EventBus.Base.Abstraction;
using EventBus.Base;
using EventBus.Factory;
using ReportService.Api.IntegrationEvents.Events;
using ReportService.Api.IntegrationEvents.EventHandlers;
using ReportService.Api.Extensions;
using ReportService.Api.Core.Application.Interfaces;
using ReportService.Api.Services;
using ReportService.Api.Infrastucture.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(conf => conf.AddConsole());
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.AddTransient<ExcelResponseReturnedFailedIntegrationEventHandler>();
builder.Services.AddTransient<ExcelResponseReturnedSuccessIntegrationEventHandler>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services.AddSingleton<IEventBus>(sp =>
{
    EventBusConfig config = new()
    {
        ConnectionRetryCount = 5,
        EventNameSuffix = "IntegrationEvent",
        SubscriberClientAppName = "ReportService",
        EventBusType = EventBusType.RabbitMQ
    };
    return EventBusFactory.Create(config, sp);
});


var app = builder.Build();
IEventBus eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<ExcelResponseReturnedSuccessIntegrationEvent, ExcelResponseReturnedSuccessIntegrationEventHandler>();
eventBus.Subscribe<ExcelResponseReturnedFailedIntegrationEvent, ExcelResponseReturnedFailedIntegrationEventHandler>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MigrateDbContext<ReportDbContext>((context, services) =>
{
    var env = services.GetService<IWebHostEnvironment>();
    var logger = services.GetService<ILogger<ReportDbContextSeed>>();

    new ReportDbContextSeed()
        .SeedAsync(context, env!, logger!)
        .Wait();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
