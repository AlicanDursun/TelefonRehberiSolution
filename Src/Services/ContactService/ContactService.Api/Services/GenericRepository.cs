using ContactService.Api.Core.Application.Interfaces.Repositories;
using ContactService.Api.Core.Application.ViewModel;
using ContactService.Api.Core.Domain;
using ContactService.Api.Infrastructure.Context;
using ContactService.Api.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContactService.Api.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity 
    {
        private readonly ContactDbContext _dbcontext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ContactDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _dbSet = _dbcontext.Set<T>();
        }
        public virtual async Task<T> AddAsync(T entity)
        {

            await _dbSet.AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
            return entity;
        }
        public virtual async Task Delete(Guid id)
        {
            var person = await GetById(id);
            _dbSet.Remove(person);
            await _dbcontext.SaveChangesAsync();
            return;
        }

        public virtual async Task<List<T>> Get(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);

            }
            return await query.FirstOrDefaultAsync(w => w.Id == id);
        }

       

        public virtual async Task<T> Update(T entity)
        {
            _dbSet.Update(entity);
            await _dbcontext.SaveChangesAsync();
            return entity;
        }


    }
}
