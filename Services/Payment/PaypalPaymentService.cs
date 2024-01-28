using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services.Payment
{
    public class PaypalPaymentService : PaymentServiceAbstract
    {
        public PaypalPaymentService(TestDbContext ctx, IRepository<Customer> customerRepository, IRepository<Order> orderRepository)
        {
            _ctx = ctx;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public override Order ProcessPayment(decimal value, int customerId)
        {
            // Implementação para pagamentos por pay pal (descontando 5% no valor)
            value -= 5.00m / 100.00m * value;
            Console.WriteLine("PAGAMENTO POR PAY PAL");

            return ProccessCustomer(value, customerId);
        }
    }
}
