using GRA.Domain.Entities;

namespace GRA.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBaseReadOnly<T> where T : class
    {
        T? GetById(int entityId);
        Y? GetById<Y>(int entityId, bool isDeleted) where Y : EntityBase;

        Task<T?> GetByIdAsync(int entityId);
        Task<Y?> GetByIdAsync<Y>(int entityId, bool isDeleted) where Y : EntityBase;

        IQueryable<T> GetAll();
        IList<Y> GetAll<Y>(bool isDeleted) where Y : EntityBase;

        Task<IList<T>> GetAllAsync();
        Task<IList<Y>> GetAllAsync<Y>(bool isDeleted) where Y : EntityBase;
    }
}
