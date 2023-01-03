using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.UnitTest.Events.Events
{
    public class ExcelRequestCreatedIntegrationEvent:IntegrationEvent
    {
        public new int Id { get; set; }

        public ExcelRequestCreatedIntegrationEvent(int id)
        {
            Id = id;
        }
    }
}
