namespace ProvaPub.Services
{
    public interface IService<T>
    {
        List<T> List(int page);
    }
}
