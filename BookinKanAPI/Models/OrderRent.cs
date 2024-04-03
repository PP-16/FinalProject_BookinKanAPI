using BookinKanAPI.Data;
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
        public bool ConfirmReturn { get; set; }

        public ICollection<OrderRentItem> OrderRentItems { get; set; } = new List<OrderRentItem>();
        public long GetTotalPrice()
        {
            long total = 0;

            // Calculate total price based on quantity and car price
            foreach (var item in OrderRentItems)
            {
                total += item.Quantity * item.CarsPrice;

                // Check if a Driver is assigned in OrderRentItem
                if (item.DriverId.HasValue)
                {
                    total += (long)item.Driver.Charges;
                }
            }

            // Calculate total price based on the number of days between pickup and return
            foreach (var item in OrderRentItems)
            {
                // Calculate the number of days between pickup and return
                int days = (int)(item.DateTimeReturn - item.DateTimePickup).TotalDays;

                // Multiply the total by the number of days for each item
                total += days * (item.Quantity * item.CarsPrice);

                // Include driver charges for each day if applicable
                if (item.DriverId.HasValue)
                {
                    total += days * (long)item.Driver.Charges;
                }
            }

            return total;
        }
    }
}
