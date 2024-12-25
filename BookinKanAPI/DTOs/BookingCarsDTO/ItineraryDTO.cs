using BookinKanAPI.Models;

namespace BookinKanAPI.DTOs.BookingCarsDTO
{
    public class ItineraryDTO
    {
        public int ItineraryId { get; set; }
        public DateTime IssueTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool isUse { get; set; }
        public int RouteCarsId { get; set; }
        public int CarsId { get; set; }
    }
}

