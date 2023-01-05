using EventBus.Base.Abstraction;
using Newtonsoft.Json;
using OfficeOpenXml;
using ReportService.Api.Core.Application.Interfaces;
using ReportService.Api.Core.Domain;
using ReportService.Api.IntegrationEvents.Events;
using System.Data;
using System.IO;

namespace ReportService.Api.IntegrationEvents.EventHandlers
{
    public class ExcelResponseReturnedSuccessIntegrationEventHandler : IIntegrationEventHandler<ExcelResponseReturnedSuccessIntegrationEvent>
    {
        private readonly ILogger<ExcelResponseReturnedSuccessIntegrationEventHandler> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ExcelResponseReturnedSuccessIntegrationEventHandler(
            ILogger<ExcelResponseReturnedSuccessIntegrationEventHandler> logger
            , IServiceScopeFactory serviceScopeFactory)
        {

            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task Handle(ExcelResponseReturnedSuccessIntegrationEvent @event)
        {
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
                
                var ws = excelPack.Workbook.Worksheets.Add("Test").Cells[1, 1].LoadFromCollection(
                    new List<LocationReport> {
                        new LocationReport{
                            Id = @event.Id,
                            CreateDate =@event.CreatedDate,
                            Location = @event.Location,
                            PersonCount = @event.PersonCount,
                            PhoneNumberCount = @event.PhoneNumberCount
                        }
                    }, true);
                ws.SetCellValue(1, 1, Convert.ToString(@event.CreatedDate));
                ws.AutoFitColumns();
                excelPack.SaveAs(new FileInfo(setupDirPath + ".xls"));
            }
            using var scope = _serviceScopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetService<IReportRepository>();
            var dbResult = await db.AddAsync(new LocationReport
            {
                Id = @event.Id,
                CreateDate = @event.CreatedDate,
                Location = @event.Location,
                PersonCount = @event.PersonCount,
                PhoneNumberCount = @event.PhoneNumberCount
            });
            _logger.LogInformation($"Excel Data Returned Successfully with ReportId:{@event.Id}");
          
        }


    }
}
