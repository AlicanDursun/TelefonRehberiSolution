using Microsoft.EntityFrameworkCore;
using ReportService.Api.Core.Application.Interfaces;
using ReportService.Api.Core.Domain;
using ReportService.Api.Infrastucture.Context;

namespace ReportService.Api.Services
{
    public class ReportRepository : IReportRepository
    {
        private readonly ReportDbContext _reportDbContext;

        public ReportRepository(ReportDbContext reportDbContext)
        {
            _reportDbContext = reportDbContext;
        }
        public async Task<LocationReport> AddAsync(LocationReport entity)
        {
            await _reportDbContext.LocationReports.AddAsync(entity);
            await _reportDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<LocationReport>> GetAll()
        {
            return await _reportDbContext.LocationReports.ToListAsync();
        }
    }
}
