using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests : IClassFixture<TestDbContext> // Trocar o contexto "TestDbContext" por "InMemoryDbContextFixture" 
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly CustomerService _customerService;

        public CustomerServiceTests(TestDbContext fixture) // Trocar o contexto "TestDbContext" por "InMemoryDbContextFixture" 
        {
            _customerRepository = new Repository<Customer>(fixture); // Trocar "fixture" por "fixture.Context"
            _orderRepository = new Repository<Order>(fixture); // Trocar "fixture" por "fixture.Context"

            _customerService = new CustomerService(_customerRepository, _orderRepository);
        }

        [Theory]
        [InlineData(1, 150)]
        [InlineData(5, 99)]
        [InlineData(9, 88)]
        public async Task CanPurchaseAsyncValidCases(int customerId, decimal purchaseValue)
        {
            bool result = await _customerService.CanPurchase(customerId, purchaseValue);
            Assert.True(result);
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(1, 0.0)]
        public async Task CantPusrchaseByInvalidIdAndValue(int customerId, decimal purchaseValue)
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                bool result = await _customerService.CanPurchase(customerId, purchaseValue);
            });
        }

        [Theory]
        [InlineData(21, 200)]
        [InlineData(22, 10)]
        [InlineData(40, 10)]
        public async Task CantPusrchaseByCustomerDoesExist(int customerId, decimal purchaseValue)
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await _customerService.CanPurchase(customerId, purchaseValue);
            });
        }

        [Theory]
        [InlineData(11, 200)]
        [InlineData(12, 100.50)]
        [InlineData(15, 100)]
        [InlineData(17, 350)]

        public async Task CantPusrchaseByAlreadyMadePurchsaseInTheMonth(int customerId, decimal purchaseValue)
        {
            bool result = await _customerService.CanPurchase(customerId, purchaseValue);
            Assert.False(result);
        }

        [Theory]
        [InlineData(18, 199)]
        [InlineData(19, 130)]
        [InlineData(19, 100.01)]
        [InlineData(20, 101)]
        public async Task CantPusrchaseBecauseOverTheLimit(int customerId, decimal purchaseValue)
        {
            bool result = await _customerService.CanPurchase(customerId, purchaseValue);
            Assert.False(result);
        }
    }
}