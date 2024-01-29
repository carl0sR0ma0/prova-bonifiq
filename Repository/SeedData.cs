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

        public static Order[] GetOrdersSeed()
        {
            List<Order> result = new();
            Random random = new Random();
            int randomInt = random.Next(0, 350);

            for (int i = 0; i < 10; i++)
            {
                result.Add(new Order()
                {
                    Id = i + 1,
                    Value = Convert.ToDecimal(randomInt),
                    OrderDate = new DateTime(2023, 12, 20),
                    CustomerId = i + 1
                });
            }
            
            for (int i = 10; i < 18; i++)
            {
                result.Add(new Order()
                {
                    Id = i + 1,
                    Value = Convert.ToDecimal(randomInt),
                    OrderDate = new DateTime(2024, 1, 29),
                    CustomerId = i + 1
                });
            }
            return result.ToArray();
        }
    }
}
