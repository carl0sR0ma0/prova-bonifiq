using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Models.Enums;
using ProvaPub.Repository;
using ProvaPub.Services;
using ProvaPub.Services.Payment;

namespace ProvaPub.Controllers
{
    /// <summary>
    /// Esse teste simula um pagamento de uma compra.
    /// O método PayOrder aceita diversas formas de pagamento. Dentro desse método é feita uma estrutura de diversos "if" para cada um deles.
    /// Sabemos, no entanto, que esse formato não é adequado, em especial para futuras inclusões de formas de pagamento.
    /// Como você reestruturaria o método PayOrder para que ele ficasse mais aderente com as boas práticas de arquitetura de sistemas?
    /// Resposta: Eu transformei o tipo da requisição para POST e passei no corpo da requisição um objeto do tipo DataPayment que nesse
    /// objeto está passando o tipo do pagamento, o valor e o id o cliente
    /// </summary>

    [ApiController]
    [Route("[controller]")]
    public class Parte3Controller : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly TestDbContext _ctx;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Order> _orderRepository;

        public Parte3Controller(OrderService orderService, TestDbContext ctx, IRepository<Customer> customerRepository, IRepository<Order> orderRepository)
        {
            _orderService = orderService;
            _ctx = ctx;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        [HttpPost("orders")]
        public async Task<IActionResult> PlaceOrder(DataPayment payment)
        {
            // Para exemplo de identificação do pagamento vou fazer a seguinte alteração nos valores:
            // PIX => 10% de desconto
            // CARTÃO DE CRÉDITO => 5% de juros
            // PAYPAL => 5% de desconto
            var paymentStrategies = new Dictionary<TypePayment, Func<PaymentServiceAbstract>>
            {
                { TypePayment.PIX, () => new PixPaymentService(_ctx, _customerRepository, _orderRepository) },
                { TypePayment.CREDITCARD, () => new CreditCardPaymentService(_ctx, _customerRepository, _orderRepository) },
                { TypePayment.PAYPAL, () => new PaypalPaymentService(_ctx, _customerRepository, _orderRepository) }
            };

            if (paymentStrategies.TryGetValue(payment.TypePayment, out var paymentStrategyFactory))
            {
                var paymentStrategy = paymentStrategyFactory();
                return Ok(await _orderService.PayOrder(paymentStrategy, payment.Value, payment.CustomerId));
            }

            return BadRequest("Forma de pagamento inválida");
        }
    }
}
