using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportService.Api.Core.Application.Interfaces;
using ReportService.Api.Core.Domain;
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
        private readonly IReportRepository _reportRepository;

        public ReportController(IEventBus eventBus, ILogger<ReportController> logger,IReportRepository reportRepository)
        {
            _eventBus = eventBus;
            _logger = logger;
            _reportRepository = reportRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Report Service is Up and Running");
        }

        [HttpGet]
        [Route("reportsGetEvent")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetReport(string location)
        {
            await Task.Delay(1000);
            

            var eventMessage = new ExcelRequestStartedIntegrationEvent(location);

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
       
        [HttpGet]
        [Route("reportsGetList")]
        [ProducesResponseType(typeof(IEnumerable<LocationReport>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPersonsAsync()
        {
            var items = await _reportRepository.GetAll();
            return Ok(items);
        }
    }
}
