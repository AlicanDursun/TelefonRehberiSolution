using ContactService.Api.Core.Application.Interfaces.Repositories;
using ContactService.Api.Infrastructure.Context;
using ContactService.Api.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContactService.Api.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ContactDbContext _dbcontext;

        public GenericRepository(ContactDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
            return entity;
        }

      

        public async Task Delete(Guid id)
        {
            var person = await GetById(id);
            _dbcontext.Set<T>().Remove(person);
            await _dbcontext.SaveChangesAsync();
            return;
        }

        public async Task<List<T>> Get(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbcontext.Set<T>();
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbcontext.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);

            }
            return await query.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<T> Update(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
            await _dbcontext.SaveChangesAsync();
            return entity;
        }
    }
}
