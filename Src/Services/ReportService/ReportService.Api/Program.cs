using EventBus.Base.Abstraction;
using EventBus.Base;
using EventBus.Factory;
using ReportService.Api.IntegrationEvents.Events;
using ReportService.Api.IntegrationEvents.EventHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(conf => conf.AddConsole());
builder.Services.AddTransient<ExcelResponseReturnedFailedIntegrationEventHandler>();
builder.Services.AddTransient<ExcelResponseReturnedSuccessIntegrationEventHandler>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
