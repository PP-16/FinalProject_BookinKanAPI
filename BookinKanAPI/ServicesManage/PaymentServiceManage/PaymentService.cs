using Autofac.Core;
using AutoMapper;
using BookinKanAPI.Data;
using BookinKanAPI.DTOs;
using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.Migrations;
using BookinKanAPI.Models;
using BookinKanAPI.ServicesManage.ImageServiceManage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using System.Diagnostics.CodeAnalysis;

namespace BookinKanAPI.ServicesManage.PaymentServiceManage
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _config;
        private readonly DataContext _dataContext;
        private readonly IImageCarsService _imageCarsService;
        private readonly IMapper _mapper;

        public PaymentService(IConfiguration config, DataContext dataContext, IImageCarsService imageCarsService, IMapper mapper)
        {
            _config = config;
            _dataContext = dataContext;
            _imageCarsService = imageCarsService;
            _mapper = mapper;
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

        public async Task<object> CreateOrUpdatePayment(PaymentDTO payment)
        {
            var bookingPayment = await _dataContext.PaymentBookings.FirstOrDefaultAsync(b => b.BookingId == payment.BookingId);
            var booking = await _dataContext.Bookings.FindAsync(payment.BookingId);

            var order = await _dataContext.OrderRents.Include(i=>i.OrderRentItems).FirstOrDefaultAsync(p=>p.OrderRentId == payment.OrderRentId);

            if (payment.CategoryPayment == 2)
            {
                // Create a new payment intent
                var options = new PaymentIntentCreateOptions
                {
                    Amount = booking is not null ? booking.TotalPrice * 100 : order.GetTotalPrice() * 100,  // Assuming the amount is in cents
                        Currency = "THB",
                        PaymentMethodTypes = new List<string> { "card" }
                    };

                    var service = new PaymentIntentService();
                    var intent = await service.CreateAsync(options);

                    if (string.IsNullOrEmpty(intent.Id))
                    {
                        return "Failed to create payment intent.";
                    }

                    var newPaymentBooking = new PaymentBooking
                    {
                        CreateAt = DateTime.Now,
                        BookingId = payment.BookingId,
                        OrderRentId = payment.OrderRentId,
                        PymentSatus = Status.CompletePayment, // Assuming this is the default status
                        PaymentIntentId = intent.Id,
                        ClientSecret = intent.ClientSecret
                        // Set other properties as needed
                    };

                    await _dataContext.PaymentBookings.AddAsync(newPaymentBooking);
                    await _dataContext.SaveChangesAsync();

                    return newPaymentBooking;
            }
            else
            {
                // For other payment categories
                var mappedPayment = _mapper.Map<PaymentBooking>(payment);
                mappedPayment.PymentSatus = Status.Pending; // Assuming this is the default status
                mappedPayment.CreateAt = DateTime.Now;

                if (payment.ImagePayment != null)
                {
                    (string errorMessage, List<string> imageNames) = await UploadImageAsync(payment.ImagePayment);
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        return errorMessage;
                    }
                    mappedPayment.ImagePayment = imageNames[0];
                }

                await _dataContext.PaymentBookings.AddAsync(mappedPayment);
                var result = await _dataContext.SaveChangesAsync();
                if (result <= 0) return null;
                return mappedPayment;
            }
        }

        public async Task<object> UpdatePaymentStripe (int Id,int PaymentId)
        {
            var payment = await _dataContext.PaymentBookings.FirstOrDefaultAsync(i => i.PaymentBookingId == PaymentId);
            if (payment == null) return null;

            var bookingPayment = await _dataContext.PaymentBookings.FirstOrDefaultAsync(b => b.BookingId == Id );
            var orderPayment = await _dataContext.PaymentBookings.FirstOrDefaultAsync(o => o.OrderRentId == Id);

            var order = await _dataContext.OrderRents.Include(i => i.OrderRentItems).FirstOrDefaultAsync(p => p.OrderRentId == Id);

            var options = new PaymentIntentUpdateOptions
            {
                Amount = bookingPayment is not null ? bookingPayment.Booking.TotalPrice * 100 : orderPayment is not null ? order.GetTotalPrice() * 100 : null // Assuming the amount is in cents
                
            };

            var service = new PaymentIntentService();
            
            await service.UpdateAsync(payment.PaymentIntentId, options);

            // You may want to update other properties of paymentBooking as well

            return payment; // Indicate that the payment intent was updated
        }

        public async Task<string> UpdateStatusPayment(int ID,Status newStatus)
        {
            var payments = await _dataContext.PaymentBookings.FindAsync(ID);
            if (payments != null)
            {
                payments.PymentSatus = newStatus;

            }

            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";

            return null;
        }
        public async Task<(string errorMessage, List<string> imageNames)> UploadImageAsync(IFormFileCollection formFiles)
        {
            var errorMessege = string.Empty;
            var ImgName = new List<string>();

            if (_imageCarsService.IsUpload(formFiles))
            {
                errorMessege = _imageCarsService.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessege))
                {
                    ImgName = await _imageCarsService.UploadImages(formFiles);
                }
            }
            return (errorMessege, ImgName);
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
