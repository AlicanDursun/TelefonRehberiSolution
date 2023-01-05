using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ContactService.Api.Infrastructure.Context
{
    public class ContactDbDesignFactory : IDesignTimeDbContextFactory<ContactDbContext>
    {
        public ContactDbDesignFactory()
        {

        }
        public ContactDbContext CreateDbContext(string[] args)
        {
            var connStr = "Server=c_sqlserver;Database=contact;User ID=sa;Password=password@12345#;TrustServerCertificate=true;";

            var optionsBuilder = new DbContextOptionsBuilder<ContactDbContext>()
                .UseSqlServer(connStr);
            return new ContactDbContext(optionsBuilder.Options);
        }
    }
}
