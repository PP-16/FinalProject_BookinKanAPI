using BookinKanAPI.Models;

namespace BookinKanAPI.DTOs
{
    public class PaymentDTO
    {
        public int PaymentBookingId { get; set; }
        //public DateTime? CreateAt { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? BookingId { get; set; }

        public int? OrderRentId { get; set; }
        public int? OrdersPastDueId { get; set; }
        //public Status PymentSatus { get; set; }

        public IFormFileCollection? ImagePayment { get; set; }
        public int CategoryPayment { get; set; } = 0;
    }
}
