using ContactService.Api.SeedWork;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContactService.Api.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T>  where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task Delete(Guid id);
        Task<T> Update(T entity);

        Task<T> GetById(Guid id);

        Task<List<T>> GetAll();

        Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<List<T>> Get(params Expression<Func<T, object>>[] includes);
    }
}
