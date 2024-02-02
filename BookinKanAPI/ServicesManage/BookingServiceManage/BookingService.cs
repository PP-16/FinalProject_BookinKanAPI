using AutoMapper;
using BookinKanAPI.Data;
using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.Models;
using BookinKanAPI.ServicesManage.PaymentServiceManage;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BookinKanAPI.ServicesManage.BookingServiceManage
{
    public class BookingService : IBookingService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;
        private readonly IHttpContextAccessor _httpContext;

        public BookingService(DataContext dataContext, IMapper mapper,IPaymentService paymentService, IHttpContextAccessor httpContext)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _paymentService = paymentService;
            _httpContext = httpContext;
        }

        private string GetUser() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        public async Task<object> CreateBooking(BookingDTO bookingDTO)
        {
            var mappBooking = _mapper.Map<Booking>(bookingDTO);

            var itinerary = await _dataContext.Itineraries.Include(c => c.Cars).FirstOrDefaultAsync(i => i.ItineraryId == bookingDTO.ItineraryId);
            if (itinerary == null) return null;

            // Check if any of the seats already exists
            foreach (var seatNumber in bookingDTO.SeatNumbers)
            {
                if (await CheckSeat(seatNumber, bookingDTO.DateAtBooking, bookingDTO.ItineraryId))
                {
                    return $"Seat {seatNumber} is not empty";
                }
            }
            mappBooking.CreateAt = DateTime.Now;

            await _dataContext.Bookings.AddAsync(mappBooking);
         

            var result = await _dataContext.SaveChangesAsync();
            var paymentIntent = await _paymentService.CreateOrUpdatePaymentIntent(mappBooking);
            var paymentBooking = new PaymentBooking()
            {
                CreateAt = DateTime.Now,
                BookingId = mappBooking.BookingId,
                // Set other properties as needed
            };
            if (!string.IsNullOrEmpty(paymentIntent.Id))
            {
                paymentBooking.PaymentIntentId = paymentIntent.Id;
                paymentBooking.ClientSecret = paymentIntent.ClientSecret;
            }

            await _dataContext.PaymentBookings.AddAsync(paymentBooking);
            var paymentBookingResult = await _dataContext.SaveChangesAsync();

            if (paymentBookingResult <= 0) return null;
            if (result <= 0) return null;


            return paymentBooking;
        }

        public async Task<List<Booking>> GetBooking()
        {
            return await _dataContext.Bookings
                .Include(p => p.Passenger)
                .Include(i => i.Itinerary).ThenInclude(c=>c.Cars)
                .Include(i=>i.Itinerary).ThenInclude(r=>r.RouteCars)
                .OrderByDescending(i => i.BookingId).ToListAsync();
        }

        public async Task<string> UpdateStatusBooking(int ID, Status newStatus)
        {
            var Booking = await _dataContext.Bookings.FindAsync(ID);

            if (Booking != null)
            {
                Booking.BookingStatus = newStatus;

            }

            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";

            return null;

        }

        public async Task<bool> CheckSeat(string seatNumber, DateTime dateAtBooking, int itineraryId)
        {
            var existingSeats = await _dataContext.Bookings
                .Where(s => s.SeatsSerialized.Contains(seatNumber) && s.DateAtBooking == dateAtBooking && s.ItineraryId == itineraryId)
                .ToListAsync();

            return existingSeats.Any();
        }

        public async Task<List<string>> GetSeatNumbersForDay(DateTime date, int itineraryId)
        {
            var bookingsForDay = await _dataContext.Bookings
                .Where(b => b.DateAtBooking.Date == date.Date && b.ItineraryId == itineraryId)
                .ToListAsync();

            var seatNumbersForDay = new List<string>();

            foreach (var booking in bookingsForDay)
            {
                seatNumbersForDay.AddRange(booking.SeatNumbers);
            }

            return seatNumbersForDay;
        }

        public async Task<List<Itinerary>> GetTop3Itinerary()
        {
            var topUsedItineraries = await _dataContext.Bookings

      .Include(b => b.Itinerary) // ให้ Entity Framework Core ทราบว่าต้องการนำเข้าข้อมูล Itinerary
      .GroupBy(b => b.Itinerary) // กลุ่ม Bookings ตาม Itinerary

      .OrderByDescending(group => group.Count()) // เรียงลำดับจากมากไปน้อย
      .Take(3)
      .Select(group => group.Key) // เลือก Itinerary จากกลุ่ม
      .ToListAsync();
            return topUsedItineraries;
        }

        public async Task<List<Booking>> GetBookingById(int ID)
        {
            return await _dataContext.Bookings
                .Include(n=>n.Itinerary).ThenInclude(r=>r.RouteCars)
                .Include(n=>n.Itinerary).ThenInclude(c=>c.Cars)
                .Where(i => i.BookingId == ID)
                .ToListAsync();
        }

        public async Task DeleteBookings(Booking booking)
        {
            _dataContext.Bookings.Remove(booking);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _dataContext.Bookings.AsNoTracking().FirstOrDefaultAsync(p => p.BookingId == id);
        }

        public async Task<List<string>> GetSeatNumbersForDayWithStatus(DateTime date, int itineraryId)
        {
            var seatNumbersForDay = _dataContext.Bookings
                .Where(b => b.DateAtBooking.Date == date.Date && b.ItineraryId == itineraryId && b.BookingStatus == Status.CompletePayment)
                .AsEnumerable()  // Switch to in-memory operations
                .SelectMany(b => b.SeatNumbers)
                .ToList(); // Use ToList() instead of ToToListAsync()

            return seatNumbersForDay;
        }

        public async Task<List<Booking>> GetBookingsByUser()
        {
            var bookings = await _dataContext.Bookings
                .Include(i => i.Itinerary).ThenInclude(v => v.Cars)
                .Include(i => i.Itinerary).ThenInclude(r => r.RouteCars)
                .Include(u => u.Passenger)
                .Where(b => b.Passenger.Email == GetUser()).OrderByDescending(d=>d.DateAtBooking)
                .ToListAsync();

            if (bookings == null || !bookings.Any())
            {
                return null;
            }

            return bookings;
        }

        public async Task<string> UpdateDateBooking(int ID, DateTime newDate, List<string> newSeatNumbers = null)
        {
            var booking = await _dataContext.Bookings.FindAsync(ID);

            if (booking != null)
            {
                    booking.DateAtBooking = newDate;
                if (newSeatNumbers != null)
                {
                    booking.SeatNumbers = newSeatNumbers;
                }

                booking.SeatNumbers = booking.SeatNumbers;
            }

            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";

            return null;

        }

        public async Task<object> GetBookingPayment(int bookingId)
        {
            var find =  await _dataContext.PaymentBookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);
            if (find == null) return "Notfound";
            return find;
        }
    }
}
