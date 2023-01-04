using EventBus.Base.Events;

namespace ReportService.Api.IntegrationEvents.Events
{
    public class ExcelResponseReturnedSuccessIntegrationEvent:IntegrationEvent
    {
        public new int Id { get; }

        public ExcelResponseReturnedSuccessIntegrationEvent(int id) => Id = id;
    }
}
