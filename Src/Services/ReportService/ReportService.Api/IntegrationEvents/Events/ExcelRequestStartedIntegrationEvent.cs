using EventBus.Base.Events;

namespace ReportService.Api.IntegrationEvents.Events
{
    public class ExcelRequestStartedIntegrationEvent : IntegrationEvent
    {


        public new int Id { get; set; }

        public ExcelRequestStartedIntegrationEvent()
        {

        }
        public ExcelRequestStartedIntegrationEvent(int id)
        {
            Id = id;
        }


    }
}
