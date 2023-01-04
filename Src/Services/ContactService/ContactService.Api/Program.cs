using EventBus.Base.Abstraction;
using EventBus.Base;
using ContactService.Api.IntegrationEvents.EventHandlers;
using EventBus.Factory;
using ContactService.Api.IntegrationEvents.Events;
using ContactService.Api.Extensions;
using ContactService.Api.Infrastructure.Context;
using ContactService.Api.Core.Application.Interfaces.Repositories;
using ContactService.Api.Services;
using ContactService.Api.Core.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.AddTransient<ExcelRequestStartedIntegrationEventHandler>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddSingleton<IEventBus>(sp =>
{
    EventBusConfig config = new()
    {
        ConnectionRetryCount = 5,
        EventNameSuffix = "IntegrationEvent",
        SubscriberClientAppName = "ContactService",
        EventBusType = EventBusType.RabbitMQ
    };
    return EventBusFactory.Create(config, sp);
});
var app = builder.Build();
IEventBus eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<ExcelRequestStartedIntegrationEvent, ExcelRequestStartedIntegrationEventHandler>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MigrateDbContext<ContactDbContext>((context, services) =>
{
    var env = services.GetService<IWebHostEnvironment>();
    var logger = services.GetService<ILogger<ContactDbContextSeed>>();

    new ContactDbContextSeed()
        .SeedAsync(context, env!, logger!)
        .Wait();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
