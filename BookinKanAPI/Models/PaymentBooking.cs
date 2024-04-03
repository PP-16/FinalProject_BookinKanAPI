using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookinKanAPI.Models
{
    public class PaymentBooking
    {
        [Key]
        public int PaymentBookingId { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? PaymentIntentId { get; set; } = null;
        public string? ClientSecret { get; set; } = null;

        public Status PymentSatus { get; set; }

        public string? ImagePayment { get; set; } = null;
        public CategoryPayment CategoryPayment { get; set; }

        public int? BookingId { get; set; } = null;
        [JsonIgnore]
        public Booking Booking { get; set; }


        public int? OrderRentId { get; set; } = null;
        [JsonIgnore]
        public OrderRent OrderRent { get; set; }

       
         
    }
}
