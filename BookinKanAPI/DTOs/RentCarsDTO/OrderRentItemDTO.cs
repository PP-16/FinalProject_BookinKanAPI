using BookinKanAPI.DTOs.AuthenDto;
using BookinKanAPI.Models;

namespace BookinKanAPI.DTOs.RentCarsDTO
{

    public class OrderRentItemDTO : UserRents
    {
        public List<OrderRentItems> orderRentItems { get; set; }
    }
      
    public class OrderRentItems
    {
        public int? OrderRentItemId { get; set; }
        public  int Quantity { get; set; }
        public  int ItemPrice { get; set; }
        public  DateTime DateTimePickup { get; set; }
        public  DateTime DateTimeReturn { get; set; }
        public  string PlacePickup { get; set; }
        public  string PlaceReturn { get; set; }

        public  int CarsId { get; set; }

        public int? DriverId { get; set; }

    }
}
