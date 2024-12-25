using BookinKanAPI.DTOs.AuthenDto;
using BookinKanAPI.Models;

namespace BookinKanAPI.DTOs.BookingCarsDTO
{
    public class EmployeeBookingDTO:UserRents
    {
        public int BookingId { get; set; }
        public DateTime DateAtBooking { get; set; }
        public List<string> SeatNumbers { get; set; }
        public int TotalPrice { get; set; }
        public int ItineraryId { get; set; }
        public string note { get; set; }
    }
}
