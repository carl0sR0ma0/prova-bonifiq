using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services.Pagination
{
    public class PaginationService<T> : IPaginationService<T> where T : class
    {
        public readonly IRepository<T> _repository;

        public PaginationService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<ReturnList<T>> ListAsync(int page, int pageSize)
        {
            int totalCount = await _repository.CountAsync();
            var items = _repository.GetAll().Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var lastItem = items.Count <= 0 ? default : items.Last();
            bool hasNext = items.Count > 0 && lastItem != null;

            return new ReturnList<T> { HasNext = hasNext, TotalCount = totalCount, List = items };
        }
    }
}
