using Domain.Entities;
using Domain.Interfaces.Repository;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dataset;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }
        public async Task<T> DeleteAsync(int id)
        {
            var result = _dataset.Find(id);
            if (result != null)
            {
                try
                {
                    _dataset.Remove(result);
                    await _context.SaveChangesAsync();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dataset.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dataset.FindAsync(id);
        }

        public async Task<T> InsertAsync(T entity)
        {
            try
            {
                _dataset.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var result = _dataset.Find(entity.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(entity);
                    await _context.SaveChangesAsync();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
