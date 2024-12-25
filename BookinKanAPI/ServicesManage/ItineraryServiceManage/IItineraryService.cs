using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.Models;

namespace BookinKanAPI.ServicesManage.ItineraryServiceManage
{
    public interface IItineraryService
    {
        Task<List<Itinerary>> GetItineraries();
        Task<string> CreateItineraries(ItineraryDTO itineraryDTO);
        Task<string> DeleteItinerary(Itinerary itinerary);

        Task<Itinerary> GetByIdAsync(int id);
        Task<List<Itinerary>> SearchItinerary(string Name);

        Task<string> ChangeIsuse(int Id, bool isuse);

    }
}
