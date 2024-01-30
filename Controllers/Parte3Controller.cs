using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Models.Enums;
using ProvaPub.Services;
using ProvaPub.Services.Payment.Interfaces;

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
        private readonly IPixPaymentService _pixPaymentService;
        private readonly ICreditCardPaymentService _creditCardPaymentService;
        private readonly IPaypalPaymentService _paypalPaymentService;

        public Parte3Controller(OrderService orderService, IPixPaymentService pixPaymentService, ICreditCardPaymentService creditCardPaymentService, IPaypalPaymentService paypalPaymentService)
        {
            _orderService = orderService;
            _pixPaymentService = pixPaymentService;
            _creditCardPaymentService = creditCardPaymentService;
            _paypalPaymentService = paypalPaymentService;
        }

        [HttpPost("orders")]
        public async Task<IActionResult> PlaceOrder(DataPayment payment)
        {
            // Para exemplo de identificação do pagamento vou fazer a seguinte alteração nos valores:
            // PIX => 10% de desconto
            // CARTÃO DE CRÉDITO => 5% de juros
            // PAYPAL => 5% de desconto
            Dictionary<TypePayment, Func<IPaymentService>> paymentStrategies = new()
            {
                { TypePayment.PIX, () => _pixPaymentService },
                { TypePayment.CREDITCARD, () => _creditCardPaymentService },
                { TypePayment.PAYPAL, () => _paypalPaymentService }
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
