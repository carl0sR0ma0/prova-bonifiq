using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ProvaPub.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TestDbContext? _ctx = null;

        public Repository(TestDbContext context)
        {
            this._ctx = context;
        }

        public T Create(T model)
        {
            _ctx?.Set<T>().Add(model);

            return model;
        }
        public T Update(T model)
        {
            _ctx?.Set<T>().Update(model);

            return model;
        }

        public void Delete(T entity)
        {
            _ctx?.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _ctx.Set<T>().ToList();
        }

        public IQueryable<T> GetByQuery()
        {
            return _ctx.Set<T>().AsQueryable();
        }

        public T GetById(int id)
        {
            return _ctx.Set<T>().Find(id);
        }

        public async Task<T> GetAsync(int id) => await _ctx.Set<T>().FindAsync(id);

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return _ctx.Set<T>().Where(predicate).CountAsync();
        }
    }
}
