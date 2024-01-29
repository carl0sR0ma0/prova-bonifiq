using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Testes
{
    public class CustomerServiceTests : IClassFixture<InMemoryDbContextFixture>
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly CustomerService _customerService;

        public CustomerServiceTests(InMemoryDbContextFixture fixture)
        {
            _customerRepository = new Repository<Customer>(fixture.Context);
            _orderRepository = new Repository<Order>(fixture.Context);

            _customerService = new CustomerService(_customerRepository, _orderRepository);
        }

        [Theory]
        [InlineData(1, 200)]
        public async Task CanPurchaseAsync(int customerId, decimal purchaseValue)
        {
            bool result = await _customerService.CanPurchase(customerId, purchaseValue);
            Assert.True(result);
        }
    }
}