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
            var connStr = "Server=localhost;Database=contact;User ID=sa;Password=j23xmh8v5;MultipleActiveResultSets=true;TrustServerCertificate=true;";

            var optionsBuilder = new DbContextOptionsBuilder<ContactDbContext>()
                .UseSqlServer(connStr);
            return new ContactDbContext(optionsBuilder.Options);
        }
    }
}
