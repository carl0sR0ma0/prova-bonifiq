using ProvaPub.Models;

namespace ProvaPub.Services.Pagination
{
    public interface IPaginationService<T>
    {
        Task<ReturnList<T>> ListAsync(int page, int pageSize);
    }
}
