using EventBus.Base.Events;

namespace ContactService.Api.IntegrationEvents.Events
{
    public class ExcelRequestStartedIntegrationEvent:IntegrationEvent
    {

        public int Id { get; set; }

        public ExcelRequestStartedIntegrationEvent()
        {

        }
        public ExcelRequestStartedIntegrationEvent(int id)
        {
            Id = id;
        }


    }
}
