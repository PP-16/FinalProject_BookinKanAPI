using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.Models;
using Stripe;

namespace BookinKanAPI.ServicesManage.PaymentServiceManage
{
    public interface IPaymentService
    {
        Task<PaymentIntent> CreateOrUpdatePaymentIntent(Booking booking);
        //Task<string> CreatePayment(Booking booking);

        Task<List<PaymentBooking>> GetPayment();
        Task<Refund> RefundPayment(string paymentIntentId);

    }
}
