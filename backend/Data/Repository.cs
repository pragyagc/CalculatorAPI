using System.Collections.Generic;
using System.Threading.Tasks;
using CalculatorAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculatorAPI.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CalculatorDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(CalculatorDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();   // Dynamically gets the right table
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id)!;

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Calculation>> GetAllCalculationsAsync()
        {
            return await _context.Calculations.ToListAsync();
        }

        public async Task<IEnumerable<Operation>> GetAllOperationsAsync()
        {
            return await _context.Operations.ToListAsync();
        }

    }
}
