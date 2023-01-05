using EventBus.Base.Events;

namespace ReportService.Api.IntegrationEvents.Events
{
    public class ExcelRequestStartedIntegrationEvent : IntegrationEvent
    {


        public string Location { get; set; }
      
        public ExcelRequestStartedIntegrationEvent(string location)
        {
           
            Location = location;
        }


    }
}
