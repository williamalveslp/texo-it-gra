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

        public async Task<T?> GetByIdAsync(int entityId)
        {
            return await _context.Set<T>().FindAsync(entityId);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
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
