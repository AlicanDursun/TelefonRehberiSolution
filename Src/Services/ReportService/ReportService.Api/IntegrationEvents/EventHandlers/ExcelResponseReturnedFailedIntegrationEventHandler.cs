using EventBus.Base.Abstraction;
using ReportService.Api.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;
namespace ReportService.Api.IntegrationEvents.EventHandlers
{
    public class ExcelResponseReturnedFailedIntegrationEventHandler : IIntegrationEventHandler<ExcelResponseReturnedFailedIntegrationEvent>
    {
        private readonly ILogger<ExcelResponseReturnedFailedIntegrationEventHandler> _logger;

        public ExcelResponseReturnedFailedIntegrationEventHandler(
            ILogger<ExcelResponseReturnedFailedIntegrationEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(ExcelResponseReturnedFailedIntegrationEvent @event)
        {
            _logger.LogInformation($"Excel creation failed with ReportId:{@event.Id}, ErrorMessage : {@event.ErrorMessage}");
            return Task.CompletedTask;
        }
    }
}
