using EventBus.Base.Events;

namespace ContactService.Api.IntegrationEvents.Events
{
    public class ExcelResponseReturnedFailedIntegrationEvent:IntegrationEvent
    {


        public int Id { get; }
        public string ErrorMessage { get; }

        public ExcelResponseReturnedFailedIntegrationEvent(int id, string errorMessage)
        {
            Id = id;
            ErrorMessage = errorMessage;
        }
    }
}
