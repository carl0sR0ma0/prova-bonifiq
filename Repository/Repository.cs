namespace ProvaPub.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TestDbContext? _context = null;

        public Repository(TestDbContext context)
        {
            this._context = context;
        }

        public T Create(T model)
        {
            _context?.Set<T>().Add(model);

            return model;
        }
        public T Update(T model)
        {
            _context?.Set<T>().Update(model);

            return model;
        }

        public void Delete(T entity)
        {
            _context?.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public IQueryable<T> GetByQuery()
        {
            return _context.Set<T>().AsQueryable();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
    }
}
