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

        public List<Product> List(int page)
        {
            return _ctx.Products.Skip((page - 1) * 10).Take(10).ToList();
        }

        //public ProductList ListProducts(int page)
        //{
        //    return new ProductList() { HasNext = false, TotalCount = 10, Products = _ctx.Products.Skip((page - 1) * 10).Take(10).ToList<Product>() };
        //}
    }
}