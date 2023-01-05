using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ReportService.Api.Infrastucture.Context
{
    public class ReportDbContextDesignFactory : IDesignTimeDbContextFactory<ReportDbContext>
    {
        public ReportDbContext CreateDbContext(string[] args)
        {
            var connStr = "Server=localhost;Database=report;User ID=sa;Password=j23xmh8v5;MultipleActiveResultSets=true;TrustServerCertificate=true;";

            var optionsBuilder = new DbContextOptionsBuilder<ReportDbContext>()
                .UseSqlServer(connStr);
            return new ReportDbContext(optionsBuilder.Options);
        }
    }
}
