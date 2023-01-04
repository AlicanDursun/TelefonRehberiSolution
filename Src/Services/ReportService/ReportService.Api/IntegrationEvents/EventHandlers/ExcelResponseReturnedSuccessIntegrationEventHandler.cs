using EventBus.Base.Abstraction;
using Newtonsoft.Json;
using OfficeOpenXml;
using ReportService.Api.IntegrationEvents.Events;
using System.Data;
using System.IO;

namespace ReportService.Api.IntegrationEvents.EventHandlers
{
    public class ExcelResponseReturnedSuccessIntegrationEventHandler : IIntegrationEventHandler<ExcelResponseReturnedSuccessIntegrationEvent>
    {
        private readonly ILogger<ExcelResponseReturnedSuccessIntegrationEventHandler> _logger;

        public ExcelResponseReturnedSuccessIntegrationEventHandler(
            ILogger<ExcelResponseReturnedSuccessIntegrationEventHandler> logger)
        {

            _logger = logger;
        }
        public Task Handle(ExcelResponseReturnedSuccessIntegrationEvent @event)
        {
            
            List<Person> list = new List<Person>()
            {
                new()
            {
                Id = @event.Id,
                Name = "Alican"
            }

            };

            //DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(p), typeof(DataTable));
            var contentRootPath = "Reports";
            string fileName = @event.Id.ToString();
            var setupDirPath = Path.Combine(contentRootPath, fileName);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (!Directory.Exists(contentRootPath))
            {
                Directory.CreateDirectory(contentRootPath);
            }
            using (ExcelPackage excelPack = new ExcelPackage(setupDirPath))
            {
                var ws = excelPack.Workbook.Worksheets.Add("Test").Cells[1, 1].LoadFromCollection(list, true);


                excelPack.SaveAs(new FileInfo(setupDirPath+".xls"));
            }
            _logger.LogInformation($"Excel Data Returned Successfully with ReportId:{@event.Id}");
            return Task.CompletedTask;
        }

        public class Person
        {
            public int Id { get; set; }

            public string Name { get; set; } = "Alican";
        }
    }
}
