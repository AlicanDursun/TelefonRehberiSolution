using EventBus.Base.Events;

namespace ContactService.Api.IntegrationEvents.Events
{
    public class ExcelRequestStartedIntegrationEvent:IntegrationEvent
    {


       
        public string Location { get; set; }

        
        public ExcelRequestStartedIntegrationEvent( string location)
        {
          
            Location = location;
        }


    }
}
