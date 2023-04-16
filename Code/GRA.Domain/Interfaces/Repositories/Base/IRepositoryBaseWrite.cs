namespace GRA.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBaseWrite<T> where T : class
    {
        void Add(T entity);
        Task AddAsync(T entity);

        T InsertSave(T entity);
        Task<T> InsertSaveAsync(T entity);
        Task<IEnumerable<T>> InsertSaveInBatchAsync(IEnumerable<T> entities);

        T UpdateSave(T entity);
        Task<T> UpdateSaveAsync(T entity);

        void Remove(int entityId);

        void Remove(T entity);
    }
}
