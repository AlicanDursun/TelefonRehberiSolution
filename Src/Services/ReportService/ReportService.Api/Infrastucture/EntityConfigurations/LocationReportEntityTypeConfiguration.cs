using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportService.Api.Core.Domain;

namespace ReportService.Api.Infrastucture.EntityConfigurations
{
    public class LocationReportEntityTypeConfiguration : IEntityTypeConfiguration<LocationReport>
    {
        public void Configure(EntityTypeBuilder<LocationReport> builder)
        {
            builder.ToTable("Reports");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

        }
    }
}
