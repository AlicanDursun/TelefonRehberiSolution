using ReportService.Api.Core.Domain;

namespace ReportService.Api.Core.Application.Interfaces
{
    public interface IReportRepository
    {
        Task<LocationReport> AddAsync(LocationReport entity);

        Task<List<LocationReport>> GetAll();
    }
}
