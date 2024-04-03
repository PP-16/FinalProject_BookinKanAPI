using System.Text.Json.Serialization;

namespace BookinKanAPI.Models
{
    public class OrderRentItem
    {
        public int OrderRentItemId { get; set; }
        public int Quantity { get; set; }
        public int CarsPrice { get; set; }
        public DateTime DateTimePickup { get; set; }
        public DateTime DateTimeReturn { get; set; }
        public string PlacePickup { get; set; }
        public string PlaceReturn { get; set; }

        public int CarsId { get; set; }
        public Cars Cars { get; set; }

        public int? DriverId { get; set; }
        public Driver Driver { get; set; }

        public int OrderRentId { get; set; }
        [JsonIgnore]
        public OrderRent OrderRent { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}
