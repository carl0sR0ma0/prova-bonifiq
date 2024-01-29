using Bogus;
using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public static class SeedData
    {
        public static Customer[] GetCustomerSeed()
        {
            List<Customer> result = new();
            for (int i = 0; i < 20; i++)
            {
                result.Add(new Customer()
                {
                    Id = i + 1,
                    Name = new Faker().Person.FullName,
                });
            }
            return result.ToArray();
        }
        public static Product[] GetProductSeed()
        {
            List<Product> result = new();
            for (int i = 0; i < 20; i++)
            {
                result.Add(new Product()
                {
                    Id = i + 1,
                    Name = new Faker().Commerce.ProductName()
                });
            }
            return result.ToArray();
        }
    }
}
