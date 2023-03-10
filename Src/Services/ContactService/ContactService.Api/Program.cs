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
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((context, options) =>
{
    options.ValidateOnBuild = false;
    options.ValidateScopes = false;
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(conf => conf.AddConsole());
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.AddTransient<ExcelRequestStartedIntegrationEventHandler>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped<ILocationStatistic, LocationStatisticRepository>();
builder.Services.ConfigureConsul(builder.Configuration);
builder.Services.AddSingleton<IEventBus>(sp =>
{
    EventBusConfig config = new()
    {
        ConnectionRetryCount = 5,
        EventNameSuffix = "IntegrationEvent",
        SubscriberClientAppName = "ContactService",
        EventBusType = EventBusType.RabbitMQ,
        Connection = new ConnectionFactory()
        {
            HostName = "c_rabbitmq",
        }
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
app.RegisterWithConsul(app.Lifetime,builder.Configuration);
app.Run();
