using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ale_Ink.API.Data;

namespace Ale_Ink.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected DbSet<T> _dbset;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _dbset.ToListAsync();

        public async Task<T> GetByIdAsync(object id) =>
            await _dbset.FindAsync(id);

        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbset.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
