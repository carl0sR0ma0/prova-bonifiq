using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Payment.Interfaces;

namespace ProvaPub.Services.Payment
{
    public class PixPaymentService : PaymentServiceAbstract, IPixPaymentService
    {
        public PixPaymentService(TestDbContext ctx, IRepository<Customer> customerRepository, IRepository<Order> orderRepository)
        {
            _ctx = ctx;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }
        
        public async Task<Order?> PayOrder(decimal value, int customerId)
        {
            // Implementação para pagamentos por pix (descontando 10% no valor)
            value -= 10.00m / 100.00m * value;
            Console.WriteLine("PAGAMENTO POR PIX");

            return await ProccessCustomer(value, customerId);
        }
    }
}
