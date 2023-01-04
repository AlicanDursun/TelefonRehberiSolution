using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportService.Api.IntegrationEvents.Events;
using System.Net;

namespace ReportService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IEventBus eventBus, ILogger<ReportController> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Report Service is Up and Running");
        }

        [HttpGet]
        [Route("reports")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetReport()
        {
            await Task.Delay(1000);
            

            var eventMessage = new ExcelRequestStartedIntegrationEvent();

            try
            {
                _eventBus.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {ReportService.App}", eventMessage.Id);

                throw;
            }
            return Accepted();
        }
    }
}
