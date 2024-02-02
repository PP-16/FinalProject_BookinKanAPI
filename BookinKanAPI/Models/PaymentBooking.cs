using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookinKanAPI.Models
{
    public class PaymentBooking
    {
        [Key]
        public int PaymentBookingId { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? BookingId { get; set; }
        [JsonIgnore]
        public Booking Booking { get; set; }

        public Status PymentSatus { get; set; }

    }
}
