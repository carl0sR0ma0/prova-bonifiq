using ProvaPub.Models;
using ProvaPub.Services.Payment;

namespace ProvaPub.Services
{
    public class OrderService
	{
		public async Task<Order>? PayOrder(PaymentServiceAbstract payment, decimal paymentValue, int customerId)
		{
            return await Task.FromResult(payment.ProcessPayment(paymentValue, customerId));
		}
	}
}
