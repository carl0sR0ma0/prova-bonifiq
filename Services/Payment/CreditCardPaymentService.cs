using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Payment.Interfaces;

namespace ProvaPub.Services.Payment
{
    public class CreditCardPaymentService : PaymentServiceAbstract, ICreditCardPaymentService
    {
        public CreditCardPaymentService(TestDbContext ctx, IRepository<Customer> customerRepository, IRepository<Order> orderRepository)
        {
            _ctx = ctx;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Order?> PayOrder(decimal value, int customerId)
        {
            // Implementação para pagamentos para cartão de crédito(acrescentando 5 % no valor)
            value += 5.00m / 100.00m * value;
            Console.WriteLine("PAGAMENTO POR CARTÃO DE CRÉDITO");

            return await ProccessCustomer(value, customerId);
        }
    }
}
