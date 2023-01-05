using EventBus.Base.Events;

namespace ReportService.Api.IntegrationEvents.Events
{
    public class ExcelResponseReturnedSuccessIntegrationEvent:IntegrationEvent
    {
     
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }


        public ExcelResponseReturnedSuccessIntegrationEvent(string location, int personCount, int phoneNumberCount)

        {
        
            Location = location;
            PersonCount = personCount;
            PhoneNumberCount = phoneNumberCount;

        }
    }
}
