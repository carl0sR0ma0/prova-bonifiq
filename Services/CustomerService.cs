using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Pagination;

namespace ProvaPub.Services
{
    public class CustomerService : PaginationService<Customer>
    {
        private readonly IRepository<Order> _orderRepository;

        public CustomerService(IRepository<Customer> customerRepository, IRepository<Order> orderRepository) : base(customerRepository)
        {
            _orderRepository = orderRepository;
        }

        public override async Task<ReturnList<Customer>> ListAsync(int page, int pageSize)
        {
            var customers = await _repository.GetByQuery().Include(c => c.Orders)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            int totalCount = await _repository.CountAsync();
            var lastItem = customers.Count > 0 ? customers.Last() : default;
            bool hasNext = customers.Count > 0 && lastItem != null;

            return new ReturnList<Customer> { HasNext = hasNext, TotalCount = totalCount, List = customers };
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            //Business Rule: Non registered Customers cannot purchase
            var customer = await _repository.GetAsync(customerId) ?? throw new InvalidOperationException($"Customer Id {customerId} does not exists");

            //Business Rule: A customer can purchase only a single time per month
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = await _orderRepository.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            if (ordersInThisMonth > 0)
                return false;

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = await _repository.CountAsync(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                return false;

            return true;
        }
    }
}
