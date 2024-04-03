using System.ComponentModel.DataAnnotations.Schema;

namespace BookinKanAPI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime DateAtBooking { get; set; }

        [NotMapped]
        public List<string> SeatNumbers { get; set; } = new List<string>();
        public string SeatsSerialized
        {
            get => string.Join(",", SeatNumbers);
            set => SeatNumbers = value.Split(',').ToList();
        }
        public int TotalPrice { get; set; }
        public Status BookingStatus { get; set; }

        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        public int ItineraryId { get; set; }
        public Itinerary Itinerary { get; set; }
        public DateTime? CreateAt { get; set; }

        public bool CheckIn { get; set; }
    }
}
