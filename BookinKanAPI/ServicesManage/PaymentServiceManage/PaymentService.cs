using BookinKanAPI.Data;
using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.Migrations;
using BookinKanAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;

namespace BookinKanAPI.ServicesManage.PaymentServiceManage
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _config;
        private readonly DataContext _dataContext;
        private readonly IOptions<StripeSettings> _stripeOptions;

        public PaymentService(IConfiguration config,DataContext dataContext, IOptions<StripeSettings> stripeOptions)
        {
            _config = config;
            _dataContext = dataContext;
            _stripeOptions = stripeOptions;
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

        }

        public async Task<Refund> RefundPayment(string paymentIntentId)
        {
            var refundService = new RefundService();
            var refundOptions = new RefundCreateOptions
            {
                PaymentIntent = paymentIntentId,
            };
            return await refundService.CreateAsync(refundOptions);
        }



        public async Task<PaymentIntent> CreateOrUpdatePaymentIntent(Booking booking)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"]; 
            var service = new PaymentIntentService();
            var intent = new PaymentIntent();
            var paymentBooking = _dataContext.PaymentBookings.FirstOrDefault(pb => pb.BookingId == booking.BookingId);

            if (paymentBooking == null)
            {
                // Create a new payment intent
                var options = new PaymentIntentCreateOptions
                {
                    Amount = booking.TotalPrice * 100, // Assuming the amount is in cents
                    Currency = "THB",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                intent = await service.CreateAsync(options);
                return intent;
            }
            else
            {
                // Update the existing payment intent
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = booking.TotalPrice * 100, // Assuming the amount is in cents
                };

                await service.UpdateAsync(paymentBooking.PaymentIntentId, options);

                // You may want to update other properties of paymentBooking as well

                return null; // Indicate that the payment intent was updated
            }
        }

        public async Task<List<PaymentBooking>> GetPayment()
        {
            return await _dataContext.PaymentBookings.OrderByDescending(i => i.PaymentBookingId).ToListAsync();
        }



        //public async Task<string> CreatePayment(Booking booking)
        //{

        //    var intent = await CreateOrUpdatePaymentIntent(booking);
        //    var newPaymentBooking = new PaymentBooking()
        //    {
        //        CreateAt = DateTime.Now,
        //        BookingId = booking.BookingId,
        //        // You may need to set other properties as well
        //    };

        //    if (!string.IsNullOrEmpty(intent.Id))
        //    {

        //        newPaymentBooking.PaymentIntentId = intent.Id;
        //        newPaymentBooking.ClientSecret = intent.ClientSecret;
        //        // You may need to set other properties as well

        //    }

        //    _dataContext.PaymentBookings.Add(newPaymentBooking);
        //    var result = await _dataContext.SaveChangesAsync();

        //    if (result == 0)
        //    {
        //        return "Payment information saved successfully";
        //    }
        //    else
        //    {
        //        return "Error saving payment information to the database";
        //    }

        //}

    }
}
