using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class ProductService : IService<Product>
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public ReturnList<Product> List(int page)
        {
            List<Product> products = (from p in _productRepository.GetAll()
                                      select p).Skip((page - 1) * 10).Take(10).ToList();

            int count = _productRepository.GetAll().ToList().Count;
            var lastItem = products.Count > 0 ? products.Last() : new Product() { Id = 0 };
            bool hasNext = products.Count > 0 && lastItem.Id + 1 < count;

            return new ReturnList<Product>() { HasNext = hasNext, TotalCount = count, List = products };
        }
    }
}