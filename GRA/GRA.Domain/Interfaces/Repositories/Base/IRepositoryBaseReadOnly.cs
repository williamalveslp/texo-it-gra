using GRA.Domain.Entities;

namespace GRA.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBaseReadOnly<T> where T : class
    {
        T? GetById(int entityId);
        Task<T?> GetByIdAsync(int entityId);

        IQueryable<T> GetAll();
        Task<IList<T>> GetAllAsync();
    }
}
