using BookinKanAPI.DTOs;
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
        Task<object> CreateOrUpdatePayment(PaymentDTO payment);
        Task<string> UpdateStatusPayment(int ID, Status newStatus);
        Task<object> UpdatePaymentStripe(int Id, int PaymentId);

    }
}
