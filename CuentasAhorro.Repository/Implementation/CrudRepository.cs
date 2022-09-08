using CuentasAhorro.Repository.Context;
using CuentasAhorro.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CuentasAhorro.Repository.Implementation
{
    public class CrudRepository<T> : ICrudRepository<T> where T : class
    {
        private readonly DBContext _context;

        public CrudRepository(DBContext context) => _context = context;

        public async Task<T> InsertAsync(T entity)
        {
            using (var dbTrans = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.Set<T>().AddAsync(entity);
                    await _context.SaveChangesAsync();

                    dbTrans.Commit();

                    return entity;
                }
                catch (Exception)
                {
                    dbTrans.Rollback();
                }
            }

            return null;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            bool result = false;

            using (var dbTrans = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<T>().Update(entity);
                    await _context.SaveChangesAsync();

                    dbTrans.Commit();

                    result = true;
                }
                catch (Exception)
                {
                    dbTrans.Rollback();
                }
            }

            return result;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            bool result = false;

            using (var dbTrans = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<T>().Remove(entity);
                    await _context.SaveChangesAsync();

                    dbTrans.Commit();

                    result = true;
                }
                catch (Exception)
                {
                    dbTrans.Rollback();
                }
            }

            return result;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> condition)
        {
            return await _context.Set<T>()
                .Where(condition)
                .FirstOrDefaultAsync();
        }

        public async Task<TType> GetAsync<TType>(Expression<Func<T, bool>> condition, Expression<Func<T, TType>> selection) where TType : class
        {
            return await _context.Set<T>()
               .Where(condition)
               .Select(selection)
               .FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> condition)
        {
            return await _context.Set<T>()
                .Where(condition)
                .ToListAsync();
        }

        public async Task<List<TType>> GetListAsync<TType>(Expression<Func<T, TType>> selection) where TType : class
        {
            return await _context.Set<T>()
               .Select(selection)
               .ToListAsync();
        }

        public async Task<List<TType>> GetListAsync<TType>(Expression<Func<T, bool>> condition, Expression<Func<T, TType>> selection) where TType : class
        {
            return await _context.Set<T>()
                .Where(condition)
                .Select(selection)
                .ToListAsync();
        }

        public async Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _context.Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<T>> GetPagedReponseAsync(Expression<Func<T, bool>> condition, int pageNumber, int pageSize)
        {
            return await _context.Set<T>()
                .Where(condition)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<TType>> GetPagedReponseAsync<TType>(Expression<Func<T, bool>> condition, Expression<Func<T, TType>> selection, int pageNumber, int pageSize) where TType : class
        {
            return await _context.Set<T>()
                .Where(condition)
                .Select(selection)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> condition)
        {
            return await _context.Set<T>()
                .Where(condition)
                .CountAsync();
        }
    }
}