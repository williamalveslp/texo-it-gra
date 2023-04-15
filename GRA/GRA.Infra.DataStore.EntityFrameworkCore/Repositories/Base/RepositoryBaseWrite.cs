using GRA.Domain.Interfaces.Repositories.Base;
using GRA.Infra.DataStore.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;

namespace GRA.Infra.DataStore.EntityFrameworkCore.Repositories.Base
{
    public abstract class RepositoryBaseWrite<T> : IDisposable, IRepositoryBaseWrite<T> where T : class
    {
        protected readonly DatabaseContext _context;
        protected readonly internal DbSet<T> _dbSet;

        protected RepositoryBaseWrite(DatabaseContext context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public T InsertSave(T entity)
        {
            Add(entity);
            SaveChanges();

            return entity;
        }

        public async Task<T> InsertSaveAsync(T entity)
        {
            await AddAsync(entity);
            await SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<T>> InsertSaveInLoteAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            SaveChanges();

            return entities;
        }

        public T UpdateSave(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
            return entity;
        }

        public async Task<T> UpdateSaveAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync();

            return entity;
        }

        public void Remove(int id)
        {
            var entity = _dbSet.Find(id);

            if (entity == null)
                return;

            Remove(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
            SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool dispose)
        {
            if (!dispose)
                return;

            _context.Dispose();
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
