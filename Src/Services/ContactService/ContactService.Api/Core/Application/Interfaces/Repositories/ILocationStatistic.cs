using ContactService.Api.Core.Application.ViewModel;

namespace ContactService.Api.Core.Application.Interfaces.Repositories
{
    public interface ILocationStatistic
    {
        Task<LocationDto> GetLocationStatistics(string location);
    }

}
