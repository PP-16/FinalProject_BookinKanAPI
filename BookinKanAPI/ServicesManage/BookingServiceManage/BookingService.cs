using AutoMapper;
using Azure.Core;
using BookinKanAPI.Data;
using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.Models;
using BookinKanAPI.ServicesManage.PaymentServiceManage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BookinKanAPI.ServicesManage.BookingServiceManage
{
    public class BookingService : IBookingService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public BookingService(DataContext dataContext, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _dataContext = dataContext;
            _mapper = mapper;
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
            if (result <= 0) return null;


            return mappBooking;
        }


        public async Task<Dictionary<DateTime, decimal>> GetTotalPricesByDateAtBooking()
        {
            var result = await _dataContext.Bookings
                .GroupBy(b => b.DateAtBooking.Date)
                .Select(group => new
                {
                    DateAtBooking = group.Key,
                    TotalPrice = group.Sum(b => b.TotalPrice)
                })
                .ToDictionaryAsync(x => x.DateAtBooking, x => (decimal)x.TotalPrice);

            return result;
        }

        //public async Task<Dictionary<DateTime, decimal>> GetTotalPricesByMountAtBooking(int month)
        //{
        //    var result = await _dataContext.Bookings
        //        .Where(b => b.DateAtBooking.Month == month)
        //        .GroupBy(b => new DateTime(b.DateAtBooking.Year, b.DateAtBooking.Month, 1))
        //        .Select(group => new
        //        {
        //            DateAtBooking = group.Key,
        //            TotalPrice = group.Sum(b => b.TotalPrice)
        //        })
        //        .ToDictionaryAsync(x => x.DateAtBooking, x => (decimal)x.TotalPrice);

        //    return result;
        //}
        public async Task<Dictionary<DateTime, decimal>> GetTotalPricesByMountAtBooking(int month,int year)
        {
            var result = await _dataContext.Bookings
        .Where(b => b.DateAtBooking.Month == month && b.DateAtBooking.Year == year)
        .GroupBy(b => b.DateAtBooking.Date) // Group by date only
        .Select(group => new
        {
            DateAtBooking = group.Key,
            TotalPrice = group.Any() ? group.Sum(b => b.TotalPrice) : 0 // Check if group has any elements before summing
        })
        .ToDictionaryAsync(x => x.DateAtBooking, x => (decimal)x.TotalPrice);

            return result;
        }

        public async Task<Dictionary<DateTime, decimal>> GetTotalPricesByYearAtBooking(int year)
        {
            var result = await _dataContext.Bookings
           .Where(b => b.DateAtBooking.Year == year)
           .GroupBy(b => b.DateAtBooking.Month) // Group by date only
           .Select(group => new
           {
               DateAtBooking = new DateTime(year, group.Key, 1), // Create DateTime object with year and month
               TotalPrice = group.Any() ? group.Sum(b => b.TotalPrice) : 0 // Check if group has any elements before summing
           })
           .ToDictionaryAsync(x => x.DateAtBooking, x => (decimal)x.TotalPrice);

            return result;
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
     .Include(b => b.Itinerary)
         .ThenInclude(i => i.Cars) // Include Cars navigation property
     .Include(b => b.Itinerary)
         .ThenInclude(i => i.RouteCars) // Include RouteCars navigation property
     .GroupBy(b => b.Itinerary)
     .OrderByDescending(group => group.Count())
     .Take(3)
     .Select(group => group.Key)
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


        public async Task<object> CreateBookingByEmpolyee(EmployeeBookingDTO bookingDTO)
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
            var findUser = await _dataContext.Passengers.FirstOrDefaultAsync(p => p.Email == bookingDTO.Email);
            if(findUser == null)
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(bookingDTO.Phone);
                Passenger passenger = new Passenger
                {
                    PassengerName = bookingDTO.PassengerName,
                    IDCardNumber = bookingDTO.IDCardNumber,
                    Email = bookingDTO.Email,
                    Phone = bookingDTO.Phone,
                    Password = passwordHash,
                    RoleId = 2,
                    isUse = true,

                };
                mappBooking.Passenger = passenger;
                await _dataContext.Passengers.AddAsync(passenger);
            }
            else
            {
                mappBooking.PassengerId = findUser.PassengerId;
            }

           
            mappBooking.CreateAt = DateTime.Now;
            mappBooking.BookingStatus = Status.Topay;
            



            //var passent

            await _dataContext.Bookings.AddAsync(mappBooking);


            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return null;


            return mappBooking;
        }

        public async Task<string> ChangeCheckInStatus(int Id, bool checkIn)
        {
            var booking = await _dataContext.Bookings.FindAsync(Id);
            if (booking != null)
            {
                booking.CheckIn = checkIn;
            }
            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";
            return null;
        }

    }
}
