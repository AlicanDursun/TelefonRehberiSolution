using ContactService.Api.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactService.Api.Infrastructure.EntityConfigurations
{
    public class PersonInformationEntityTypeConfiguration : IEntityTypeConfiguration<PersonInformation>
    {
        public void Configure(EntityTypeBuilder<PersonInformation> builder)
        {
            builder.ToTable("PersonInformations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

          
                
        }
    }
}
