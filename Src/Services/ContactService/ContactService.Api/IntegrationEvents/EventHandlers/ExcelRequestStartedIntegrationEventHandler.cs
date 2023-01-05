using ContactService.Api.Core.Application.Interfaces.Repositories;
using ContactService.Api.Core.Application.ViewModel;
using ContactService.Api.IntegrationEvents.Events;
using EventBus.Base.Abstraction;
using EventBus.Base.Events;
using Microsoft.Extensions.DependencyInjection;


namespace ContactService.Api.IntegrationEvents.EventHandlers
{
    public class ExcelRequestStartedIntegrationEventHandler : IIntegrationEventHandler<ExcelRequestStartedIntegrationEvent>
    {
        private readonly ILogger<ExcelRequestStartedIntegrationEventHandler> _logger;
        private readonly IEventBus _eventBus;
        //private readonly ILocationStatistic _locationStatistic;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ExcelRequestStartedIntegrationEventHandler(IEventBus eventBus,
            ILogger<ExcelRequestStartedIntegrationEventHandler> logger
           ,IServiceScopeFactory serviceScopeFactory
            )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _eventBus = eventBus;
            _logger = logger;
           
        }


        public async Task Handle(ExcelRequestStartedIntegrationEvent @event)
        {
            bool paymentSuccessFlag = true;
            using var scope = _serviceScopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetService<ILocationStatistic>();
           
            //_logger.LogInformation("----- Handling integration event: {IntegrationEventId} at ContactService.Api - ({@IntegrationEvent})", @event.Id);
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at ContactService.Api", @event.Id);
             
            var dbResult = await db.GetLocationStatistics(@event.Location);

            IntegrationEvent contactEvent = paymentSuccessFlag
                 ? new ExcelResponseReturnedSuccessIntegrationEvent(@event.Id,dbResult.Location,dbResult.PersonCount,dbResult.PhoneNumberCount)
                 : new ExcelResponseReturnedFailedIntegrationEvent(@event.Id, "This is a fake error message");
            _eventBus.Publish(contactEvent);
        }
    }
}
