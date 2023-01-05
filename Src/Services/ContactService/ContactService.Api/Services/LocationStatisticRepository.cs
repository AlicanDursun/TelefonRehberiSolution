using ContactService.Api.Core.Application.Interfaces.Repositories;
using ContactService.Api.Core.Application.ViewModel;
using ContactService.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContactService.Api.Services
{
    public class LocationStatisticRepository : ILocationStatistic
    {
        private readonly ContactDbContext _contactDbContext;

        public LocationStatisticRepository(ContactDbContext contactDbContext)
        {
            _contactDbContext = contactDbContext;
        }

        public async Task<LocationDto> GetLocationStatistics(string location)
        {

            var result = await _contactDbContext.PersonInformations
                .Where(w => w.Location == location)
                .GroupBy(x => x.Location)
                .Select(g => new LocationDto
                {
                    Location = location,
                    PersonCount = g.Select(w => w.PersonId).Distinct().Count(),
                    PhoneNumberCount = g.Select(w => w.PhoneNumber).Distinct().Count()
                }).FirstOrDefaultAsync();
            return  result;



        }
    }
}
