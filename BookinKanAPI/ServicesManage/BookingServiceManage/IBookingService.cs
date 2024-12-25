using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.Models;

namespace BookinKanAPI.ServicesManage.BookingServiceManage
{
    public interface IBookingService
    {
        Task<List<Booking>> GetBooking();
      Task<object> CreateBooking(BookingDTO bookingDTO);
        Task<string> UpdateStatusBooking(int ID, Status newStatus);
      Task<bool> CheckSeat(string seatNumber, DateTime dateAtBooking, int itineraryId);
        Task<List<string>> GetSeatNumbersForDay(DateTime date,int itineraryId);
        Task<List<Itinerary>> GetTop3Itinerary();
        Task<List<Booking>> GetBookingById(int ID);
        Task DeleteBookings(Booking booking);
        Task<Booking> GetByIdAsync(int id);

        Task<List<string>> GetSeatNumbersForDayWithStatus(DateTime date, int itineraryId);
        Task<List<Booking>> GetBookingsByUser();
        Task<object> GetBookingPayment(int bookingId);
        Task<string> UpdateDateBooking(int ID, DateTime newDate, List<string> newSeatNumbers = null);
        Task<object> CreateBookingByEmpolyee(EmployeeBookingDTO bookingDTO);
        Task<string> ChangeCheckInStatus(int Id, bool checkIn);
        Task<Dictionary<DateTime, decimal>> GetTotalPricesByDateAtBooking();
        Task<Dictionary<DateTime, decimal>> GetTotalPricesByMountAtBooking(int month, int year);
        Task<Dictionary<DateTime, decimal>> GetTotalPricesByYearAtBooking(int year);
        Task<object> CheckAndUpdateBookingStatus();
        Task<List<Booking>> GetBookingfromItinerary(int itineraryId, DateTime dateBooking);
    }
}
