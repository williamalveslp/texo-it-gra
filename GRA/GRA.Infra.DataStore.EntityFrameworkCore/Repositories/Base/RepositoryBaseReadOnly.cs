using GRA.Domain.Entities;
using GRA.Domain.Interfaces.Repositories.Base;
using GRA.Infra.DataStore.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;

namespace GRA.Infra.DataStore.EntityFrameworkCore.Repositories.Base
{
    public abstract class RepositoryBaseReadOnly<T> : IDisposable, IRepositoryBaseReadOnly<T> where T : class
    {
        protected readonly DatabaseContext _context;
        protected readonly internal DbSet<T> _dbSet;

        protected RepositoryBaseReadOnly(DatabaseContext context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }

        public T? GetById(int entityId)
        {
            return _context.Set<T>().Find(entityId);
        }

        public Y? GetById<Y>(int entityId, bool isDeleted) where Y : EntityBase
        {
            return _context.Set<Y>().Where(x => x.Id == entityId)
                                    .FirstOrDefault();
        }

        public async Task<T?> GetByIdAsync(int entityId)
        {
            return await _context.Set<T>().FindAsync(entityId);
        }

        public async Task<Y?> GetByIdAsync<Y>(int entityId, bool isDeleted) where Y : EntityBase
        {
            return await _context.Set<Y>().Where(x => x.Id == entityId)
                                          .FirstOrDefaultAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public IList<Y> GetAll<Y>(bool isDeleted) where Y : EntityBase
        {
            return _context.Set<Y>().ToList();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IList<Y>> GetAllAsync<Y>(bool isDeleted) where Y : EntityBase
        {
            return await _context.Set<Y>().ToListAsync();
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
    }
}
