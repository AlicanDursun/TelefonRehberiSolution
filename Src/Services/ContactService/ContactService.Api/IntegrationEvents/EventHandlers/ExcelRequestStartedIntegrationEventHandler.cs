using ContactService.Api.IntegrationEvents.Events;
using EventBus.Base.Abstraction;
using EventBus.Base.Events;


namespace ContactService.Api.IntegrationEvents.EventHandlers
{
    public class ExcelRequestStartedIntegrationEventHandler : IIntegrationEventHandler<ExcelRequestStartedIntegrationEvent>
    {
        private readonly ILogger<ExcelRequestStartedIntegrationEventHandler> _logger;
        private readonly IEventBus _eventBus;
        public ExcelRequestStartedIntegrationEventHandler(IEventBus eventBus,
            ILogger<ExcelRequestStartedIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }


        public Task Handle(ExcelRequestStartedIntegrationEvent @event)
        {
            bool paymentSuccessFlag = true;
            //_logger.LogInformation("----- Handling integration event: {IntegrationEventId} at ContactService.Api - ({@IntegrationEvent})", @event.Id);
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at ContactService.Api", @event.Id);
            IntegrationEvent contactEvent = paymentSuccessFlag
                 ? new ExcelResponseReturnedSuccessIntegrationEvent(@event.Id)
                 : new ExcelResponseReturnedFailedIntegrationEvent(@event.Id, "This is a fake error message");
            _eventBus.Publish(contactEvent);
            return Task.CompletedTask;
        }
    }
}
