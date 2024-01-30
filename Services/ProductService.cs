using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Pagination;

namespace ProvaPub.Services
{
    public class ProductService : PaginationService<Product>
    {
        public ProductService(IRepository<Product> productRepository) : base(productRepository)
        {
        }
    }
}