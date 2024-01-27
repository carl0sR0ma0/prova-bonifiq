using ProvaPub.Models;

namespace ProvaPub.Services
{
    public interface IService<T>
    {
        ReturnList<T> List(int page);
    }
}
