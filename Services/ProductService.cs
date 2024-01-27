using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class ProductService : IService<Product>
    {
        TestDbContext _ctx;

        public ProductService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public ReturnList<Product> List(int page)
        {
            IQueryable<Product> query = _ctx.Products;

            int count = query.Count();
            var items = query.Skip((page - 1) * 10).Take(10).ToList();
            var lastItem = items.Last();

            return new ReturnList<Product>() { HasNext = lastItem.Id + 1 < count, TotalCount = count, List = items };
        }
    }
}