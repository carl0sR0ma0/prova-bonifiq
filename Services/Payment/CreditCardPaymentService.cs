using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services.Payment
{
    public class CreditCardPaymentService : PaymentServiceAbstract
    {
        public CreditCardPaymentService(TestDbContext ctx, IRepository<Customer> customerRepository, IRepository<Order> orderRepository)
        {
            _ctx = ctx;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public override Order ProcessPayment(decimal value, int customerId)
        {
            // Implementação para pagamentos para cartão de crédito (acrescentando 5% no valor)
            value += 5.00m / 100.00m * value;
            Console.WriteLine("PAGAMENTO POR CARTÃO DE CRÉDITO");

            return ProccessCustomer(value, customerId);
        }
    }
}
