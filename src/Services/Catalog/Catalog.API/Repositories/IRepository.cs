namespace Catalog.API.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetByNameAsync(string name);
        Task<IEnumerable<T>> GetByCategoryAsync(string category);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string id);
    }
}
