#nullable disable

namespace DataAccess.Repositories.IRepository
{
    public interface IRepositoryBase
    {
        List<T> GetAll<T>(Func<T, bool> isDeleted, string[] includes = null) where T : class, new();
        IQueryable<T> GetAsync<T>(long id, string[] includes = null) where T : class, new();
        Task<T> CreateAsync<T>(T entity) where T : class, new();
        Task<bool> UpdateAsync<T>(long id, T entity) where T : class, new();
        Task<bool> DeactivateAsync<T>(long id) where T : class, new();
        Task<List<T>> CreateListAsync<T>(List<T> entity) where T : class, new();
    }
}