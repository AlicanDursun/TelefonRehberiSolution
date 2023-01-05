using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseKestrel().UseContentRoot(Directory.GetCurrentDirectory())
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config
        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
        .AddJsonFile("Configurations/ocelot.json", true, true)
        .AddEnvironmentVariables();
    });


builder.Host.UseDefaultServiceProvider((context, options) =>
{
    options.ValidateOnBuild = false;
    options.ValidateScopes = false;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot().AddConsul();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
await app.UseOcelot();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
