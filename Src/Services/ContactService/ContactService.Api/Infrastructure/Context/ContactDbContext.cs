using ContactService.Api.Core.Domain;
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
            base.OnModelCreating(modelBuilder);
        }
    }
}
