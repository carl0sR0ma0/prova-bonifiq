using System.Linq.Expressions;

namespace ProvaPub.Repository
{
    public interface IRepository<T> where T : class
    {
        T Create(T model);
        T Update(T model);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        IQueryable<T> GetByQuery();
        T GetById(int id);
        Task<T> GetAsync(int id);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
