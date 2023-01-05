using EventBus.Base.Events;

namespace ReportService.Api.IntegrationEvents.Events
{
    public class ExcelResponseReturnedFailedIntegrationEvent : IntegrationEvent
    {

       
        public string ErrorMessage { get; }

        public ExcelResponseReturnedFailedIntegrationEvent(Guid id, string errorMessage)
        {
          
            ErrorMessage = errorMessage;
        }
    }
}
