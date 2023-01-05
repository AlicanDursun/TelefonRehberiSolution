using ContactService.Api.Core.Domain;
using ContactService.Api.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace ContactService.Api.Infrastructure.Context
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonInformation> PersonInformations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonInformationEntityTypeConfiguration());
        }
    }
}
