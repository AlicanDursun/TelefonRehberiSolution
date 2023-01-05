using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ReportService.Api.Infrastucture.Context
{
    public class ReportDbContextDesignFactory : IDesignTimeDbContextFactory<ReportDbContext>
    {
        public ReportDbContext CreateDbContext(string[] args)
        {
            var connStr = "Server=c_sqlserver;Database=report;User ID=sa;Password=password@12345#;TrustServerCertificate=true;";

            var optionsBuilder = new DbContextOptionsBuilder<ReportDbContext>()
                .UseSqlServer(connStr);
            return new ReportDbContext(optionsBuilder.Options);
        }
    }
}
