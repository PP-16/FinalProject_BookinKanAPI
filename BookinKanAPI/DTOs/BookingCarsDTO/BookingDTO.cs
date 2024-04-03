using BookinKanAPI.DTOs.AuthenDto;
using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.Models;

namespace BookinKanAPI.DTOs.BookingCarsDTO
{

    public class BookingDTO
    {
        public int BookingId { get; set; }
        public DateTime DateAtBooking { get; set; }
        public List<string> SeatNumbers { get; set; }
        public int TotalPrice { get; set; }
        public Status BookingStatus { get; set; }

        public int PassengerId { get; set; }

        public int ItineraryId { get; set; }

        public bool CheckIn { get; set; } = false;
    }
}
