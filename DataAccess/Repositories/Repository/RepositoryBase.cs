using DataAccess.Context;
using DataAccess.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataAccess.Repositories.Repository
{
    public class RepositoryBase : IRepositoryBase
    {
        protected readonly CoreFunctionalityContext _context;

        public RepositoryBase(CoreFunctionalityContext context)
        {
            _context = context;
        }

        public List<T> GetAll<T>(Func<T, bool> isDeleted, string[] includes = null) where T : class, new()
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                    query = query.Include(includeProperty);
            }

            query = query.Where(isDeleted).AsQueryable();

            return query.ToList();
        }

        public IQueryable<T> GetAsync<T>(long id, string[] includes = null) where T : class, new()
        {
            var query = _context.Set<T>().Where(e => EF.Property<long>(e, "Id") == id);

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                    query = query.Include(includeProperty);
            }

            return query;
        }

        public async Task<T> CreateAsync<T>(T entity) where T : class, new()
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> UpdateAsync<T>(long id, T entity) where T : class, new()
        {
            var existingEntity = await _context.Set<T>().FindAsync(id) ?? throw new NullReferenceException();
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeactivateAsync<T>(long id) where T : class, new()
        {
            var entity = await _context.Set<T>().FindAsync(id) ?? throw new NullReferenceException();
            typeof(T).GetProperty("IsDeleted").SetValue(entity, true);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<T>> CreateListAsync<T>(List<T> entity) where T : class, new()
        {
            _context.Set<T>().AddRange(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}