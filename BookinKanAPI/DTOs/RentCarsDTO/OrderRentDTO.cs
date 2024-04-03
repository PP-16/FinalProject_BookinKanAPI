using BookinKanAPI.Models;
using System.Text.Json.Serialization;

namespace BookinKanAPI.DTOs.RentCarsDTO
{
    public class OrderRentDTO
    {
        public int? OrderRentId { get; set; }
        public  Status OrderSatus { get; set; } = 0;
        public  DateTime PaymentDate { get; set; }
        public int PassengerId { get; set; }
        public bool ConfirmReturn { get; set; } = false;
    }
}
