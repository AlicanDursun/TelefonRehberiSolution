using Microsoft.EntityFrameworkCore;
using ReportService.Api.Core.Domain;
using ReportService.Api.Infrastucture.EntityConfigurations;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ReportService.Api.Infrastucture.Context
{
    public class ReportDbContext:DbContext
    {
        public ReportDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<LocationReport> LocationReports { get; set; }
       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LocationReportEntityTypeConfiguration());
        }
    }
}
