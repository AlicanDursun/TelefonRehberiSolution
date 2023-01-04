using Microsoft.Data.SqlClient;
using Polly;

namespace ContactService.Api.Infrastructure.Context
{
    public class ContactDbContextSeed
    {
        public async Task SeedAsync(ContactDbContext context, IWebHostEnvironment env, ILogger<ContactDbContextSeed> logger)
        {
            var policy = Policy.Handle<SqlException>()
                .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, cxt) =>
                {
                    logger.LogWarning(exception, $"Exception {exception} with message {exception.Message} detected on attempt {retry}");
                }
                );
            var setupDirPath = Path.Combine(env.ContentRootPath, "Infrastructure", "Setup", "SeedFiles");
            var picturePath = "Pics";
            await policy.ExecuteAsync(() => ProcessSeeding(context, setupDirPath, picturePath, logger));
        }
        private async Task ProcessSeeding(ContactDbContext context, string setupDirPath, string picturePath, ILogger logger)
        {
            if (!context.Persons.Any())
            {
                await context.SaveChangesAsync();
            }
            if (!context.PersonInformations.Any())
            {
                await context.SaveChangesAsync();
            }
           
        }
    }
}
