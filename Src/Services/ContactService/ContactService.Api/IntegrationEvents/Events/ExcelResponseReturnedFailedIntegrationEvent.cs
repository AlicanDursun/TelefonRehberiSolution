using EventBus.Base.Events;

namespace ContactService.Api.IntegrationEvents.Events
{
    public class ExcelResponseReturnedFailedIntegrationEvent:IntegrationEvent
    {


    
        public string ErrorMessage { get; }

        public ExcelResponseReturnedFailedIntegrationEvent(Guid id, string errorMessage)
        {
            Id = id;
            ErrorMessage = errorMessage;
        }
    }
}
