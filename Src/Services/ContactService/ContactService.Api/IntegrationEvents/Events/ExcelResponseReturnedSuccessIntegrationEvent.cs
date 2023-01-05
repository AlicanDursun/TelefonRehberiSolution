using EventBus.Base.Events;

namespace ContactService.Api.IntegrationEvents.Events
{
    public class ExcelResponseReturnedSuccessIntegrationEvent : IntegrationEvent
    {
     

      

        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }


        public ExcelResponseReturnedSuccessIntegrationEvent(Guid id, string location,int personCount,int phoneNumberCount)
            
        {
            Id = id;
            Location = location;
            PersonCount = personCount;
            PhoneNumberCount = phoneNumberCount;
            
        }
    }
}
