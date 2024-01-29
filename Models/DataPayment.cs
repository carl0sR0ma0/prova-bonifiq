using ProvaPub.Models.Enums;

namespace ProvaPub.Models
{
    public class DataPayment
    {
        public TypePayment TypePayment { get; set; }
        public decimal Value { get; set; }
        public int CustomerId { get; set; }
    }
}
