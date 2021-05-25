using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dottle.Persistence.Repository
{
    public interface IRepository<T> where T : class
    {
        public IQueryable<T> GetAll();

        public IQueryable<T> GetAllInclude(string path);

        public ValueTask<T> GetByIdAsync(params object[] keyValues);

        public Task<T> GetByIdIncludes(Expression<Func<T, bool>> predicate, string includes = "");

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        
        public ValueTask<EntityEntry<T>> InsertAsync(T entity);
        
        public void Delete(T entity);
        
        public Task SaveAsync();
    }
}
