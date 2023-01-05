using Microsoft.Data.SqlClient;
using Polly;

namespace ReportService.Api.Infrastucture.Context
{
    public class ReportDbContextSeed
    {
        public async Task SeedAsync(ReportDbContext context, IWebHostEnvironment env, ILogger<ReportDbContextSeed> logger)
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

            await policy.ExecuteAsync(() => ProcessSeeding(context, logger));
        }
        private async Task ProcessSeeding(ReportDbContext context, ILogger logger)
        {
            if (!context.LocationReports.Any())
            {
                await context.SaveChangesAsync();
            }


        }
    }
}
