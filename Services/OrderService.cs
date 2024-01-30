using ProvaPub.Models;
using ProvaPub.Services.Payment.Interfaces;

namespace ProvaPub.Services
{
    public class OrderService
	{
		public async Task<Order?> PayOrder(IPaymentService payment, decimal paymentValue, int customerId)
		{
			return await payment.PayOrder(paymentValue, customerId);
        }
	}
}
