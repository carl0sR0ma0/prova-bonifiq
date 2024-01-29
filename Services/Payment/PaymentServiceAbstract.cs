using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services.Payment
{
    public abstract class PaymentServiceAbstract
    {
        public TestDbContext? _ctx;
        public IRepository<Order>? _orderRepository;
        public IRepository<Customer>? _customerRepository;

        public Order? Order { get; set; }

        public abstract Order ProcessPayment(decimal value, int customerId);

        public Order? ProccessCustomer(decimal value, int customerId)
        {
            // Verifico se existe a compra no banco?
            Order newOrder = new()
            {
                Value = value,
                OrderDate = DateTime.Parse(DateTime.Now.ToLongTimeString()),
                CustomerId = customerId
            };

            _orderRepository?.Create(newOrder);
            _ctx?.SaveChanges();

            if (newOrder == null) throw new ArgumentException("Compra não realizada");

            Order? orderSaved = (from o in _orderRepository?.GetByQuery()
                                 where o.Id == newOrder.Id
                                 select o).Include(o => o.Customer).FirstOrDefault();

            if (newOrder == null) throw new ArgumentException("Compra não encontrada");

            return orderSaved;
        }
    }
}
