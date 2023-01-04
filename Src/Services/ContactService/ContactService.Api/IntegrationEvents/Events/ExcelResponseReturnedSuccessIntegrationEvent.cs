using EventBus.Base.Events;

namespace ContactService.Api.IntegrationEvents.Events
{
    public class ExcelResponseReturnedSuccessIntegrationEvent:IntegrationEvent
    {

        public int Id { get; }

        public ExcelResponseReturnedSuccessIntegrationEvent(int id) => Id = id;

    }
}
