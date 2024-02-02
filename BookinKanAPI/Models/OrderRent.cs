using System.Text.Json.Serialization;

namespace BookinKanAPI.Models
{
    public class OrderRent
    {
        public int OrderRentId { get; set; }
        public Status OrderSatus { get; set; }
        public DateTime PaymentDate { get; set; }

        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        public ICollection<OrderRentItem> OrderRentItems { get; set; } = new List<OrderRentItem>();
    }
}
