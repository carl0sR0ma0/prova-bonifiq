using ProvaPub.Models;

namespace ProvaPub.Services.Payment.Interfaces
{
    public interface IPaymentService
    {
        Task<Order?> PayOrder(decimal value, int customerId);
    }
}
