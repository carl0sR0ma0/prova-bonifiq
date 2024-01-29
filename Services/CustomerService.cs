using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class CustomerService : IService<Customer>
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Order> _orderRepository;

        public CustomerService(IRepository<Customer> customerRepository, IRepository<Order> orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public ReturnList<Customer> List(int page)
        {
            List<Customer> customers = (from c in _customerRepository.GetByQuery()
                                        select c).Include(c => c.Orders).Skip((page - 1) * 10).Take(10).ToList();

            int count = _customerRepository.GetAll().ToList().Count;
            var lastItem = customers.Count > 0 ? customers.Last() : new Customer() { Id = 0 };
            bool hasNext = customers.Count > 0 && lastItem.Id + 1 < count;

            return new ReturnList<Customer>() { HasNext = hasNext, TotalCount = count, List = customers };
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            //Business Rule: Non registered Customers cannot purchase
            var customer = await _customerRepository.GetAsync(customerId) ?? throw new InvalidOperationException($"Customer Id {customerId} does not exists");

            //Business Rule: A customer can purchase only a single time per month
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = await _orderRepository.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            if (ordersInThisMonth > 0)
                return false;

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = await _customerRepository.CountAsync(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                return false;

            return true;
        }
    }
}
