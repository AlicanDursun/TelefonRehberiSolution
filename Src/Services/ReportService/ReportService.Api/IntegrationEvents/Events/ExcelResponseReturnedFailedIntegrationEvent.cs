using EventBus.Base.Events;

namespace ReportService.Api.IntegrationEvents.Events
{
    public class ExcelResponseReturnedFailedIntegrationEvent : IntegrationEvent
    {

        public new int Id { get; }
        public string ErrorMessage { get; }

        public ExcelResponseReturnedFailedIntegrationEvent(int id, string errorMessage)
        {
            Id = id;
            ErrorMessage = errorMessage;
        }
    }
}
