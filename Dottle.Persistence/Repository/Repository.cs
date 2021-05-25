using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dottle.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _table;

        public Repository(DatabaseContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _table;
        }

        public IQueryable<T> GetAllInclude(string path)
        {
            return _table.Include(path);
        }

        public ValueTask<T> GetByIdAsync(params object[] keyValues)
        {
            return _table.FindAsync(keyValues);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return _table.Where(predicate);
        }

        public ValueTask<EntityEntry<T>> InsertAsync(T entity)
        {
            return _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<T> GetByIdIncludes(Expression<Func<T, bool>> predicate, string includes)
        {
            return _table.Include(includes).Where(predicate).FirstOrDefaultAsync();
        }
    }
}
