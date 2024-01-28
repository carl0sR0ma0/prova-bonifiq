using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services.Payment
{
    public class PixPaymentService : PaymentServiceAbstract
    {
        public PixPaymentService(TestDbContext ctx, IRepository<Customer> customerRepository, IRepository<Order> orderRepository)
        {
            _ctx = ctx;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public override Order ProcessPayment(decimal value, int customerId)
        {
            // Implementação para pagamentos por pix (descontando 10% no valor)
            value -= 10.00m / 100.00m * value;
            Console.WriteLine("PAGAMENTO POR PIX");

            return ProccessCustomer(value, customerId);
        }
    }
}
