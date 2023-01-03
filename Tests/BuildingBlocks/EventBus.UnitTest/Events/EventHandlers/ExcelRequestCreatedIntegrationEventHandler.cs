using EventBus.Base.Abstraction;
using EventBus.UnitTest.Events.Events;


namespace EventBus.UnitTest.Events.EventHandlers
{
    public class ExcelRequestCreatedIntegrationEventHandler : IIntegrationEventHandler<ExcelRequestCreatedIntegrationEvent>
    {
        public Task Handle(ExcelRequestCreatedIntegrationEvent @event)
        {
            Console.WriteLine("Handle method worked with id:" + @event.Id);
            return Task.CompletedTask;
        }
    }
}
