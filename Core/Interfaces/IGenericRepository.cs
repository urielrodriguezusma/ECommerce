using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<int> AddNewEntityAsync(T entity);

        Task<T> UpdateEntityAsync(T entity);

        Task<int> DeleteEntityAsync(T entity);

        Task<T> GetEntityWithSpec(ISpecification<T> spec);

        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
