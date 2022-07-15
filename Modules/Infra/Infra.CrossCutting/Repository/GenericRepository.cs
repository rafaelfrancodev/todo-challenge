using Infra.CrossCutting.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.CrossCutting.Repository
{
    [ExcludeFromCodeCoverage]
    public abstract class GenericRepository<TEntity, TKey, TContext> : IRepository<TEntity, TKey>, IDisposable
        where TContext : DbContext
        where TEntity : BaseEntity<TKey>
    {
        protected TContext _context;
        protected DbSet<TEntity> _db;

        public GenericRepository(TContext context)
        {
            _context = context;
            _db = _context.Set<TEntity>();
        }
        
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<TEntity> InsertAsync(TEntity item)
        {
            try
            {

                _db.Add(item);
                await _context.SaveChangesAsync();
                return item;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertAllAsync(IEnumerable<TEntity> item)
        {
            try
            {
                _db.AddRange(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<TEntity> SelectByIdAsync(TKey id)
        {
            try
            {
                Expression<Func<TEntity, bool>> predicate = t => t.Id.Equals(id);
                var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<TEntity>> SelectAllAsync()
        {
            try
            {

                var result = await _db.ToListAsync().ConfigureAwait(false);
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<TEntity>> SelectFilterAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {

                var query =
                await _db
               .Where(where)
               .OrderByDescending(ent => ent.Id)
               .AsNoTracking()
               .ToListAsync()
               .ConfigureAwait(false);

                return query;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {

                var currentEntity = await SelectByIdAsync(entity.Id).ConfigureAwait(false);
                _context.Entry(currentEntity).CurrentValues.SetValues(entity);
                return entity;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(TKey id)
        {
            try
            {

                var entity = await SelectByIdAsync(id).ConfigureAwait(false);
                if (entity != null)
                {
                    await Task.FromResult(_db.Remove(entity));
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ExistsAsync(TKey id)
        {
            var result = await SelectByIdAsync(id).ConfigureAwait(false);

            if (result != null)
            {
                return true;
            }

            return false;
        }
    }
}
