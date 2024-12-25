using AutoMapper;
using BookinKanAPI.Data;
using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookinKanAPI.ServicesManage.ItineraryServiceManage
{
    public class ItineraryService:IItineraryService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ItineraryService(DataContext dataContext,IMapper  mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<string> CreateItineraries(ItineraryDTO itineraryDTO)
        {
            var checkId = await _dataContext.Itineraries.AsNoTracking().Include(c => c.Cars).Include(r => r.RouteCars).FirstOrDefaultAsync(i => i.ItineraryId == itineraryDTO.ItineraryId);

            var mappItinerary = _mapper.Map<Itinerary>(itineraryDTO);

            if (checkId == null)
            {
                //var car = await _dataContext.Cars.FirstOrDefaultAsync(c => c.CarsId == itineraryDTO.CarsId);
                //if (car == null) return "don't have this Car";
                //if (car.CategoryCar == 0) return "This car can't creacte itinerary";
                //var routecar = await _dataContext.RouteCars.FirstOrDefaultAsync(i => i.RouteCarsId == itineraryDTO.RouteCarsId);
                //if (routecar == null) return "don't have this route";
                mappItinerary.isUse = true;
                await _dataContext.Itineraries.AddAsync(mappItinerary);
            }
            else
            {
                mappItinerary.isUse = true;
                _dataContext.Itineraries.Update(mappItinerary);
            }
            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Create or Update";
            return null;
        }

        public async Task<string> DeleteItinerary(Itinerary itinerary)
        {
            bool check = await _dataContext.Bookings.AnyAsync(b => b.ItineraryId == itinerary.ItineraryId);

            if (check)
            {
                return "มีคนใช้อยู่";
            }

            // Itinerary is not in use, proceed with deletion
            _dataContext.Itineraries.Remove(itinerary);
            await _dataContext.SaveChangesAsync();
            return null;
        }

        public async Task<Itinerary> GetByIdAsync(int id)
        {
            return await _dataContext.Itineraries.Include(c=>c.Cars).Include(r=>r.RouteCars).FirstOrDefaultAsync(i => i.ItineraryId == id);
        }

        public async Task<List<Itinerary>> GetItineraries()
        {
            return await _dataContext.Itineraries.Include(c=>c.Cars).Include(r=>r.RouteCars).OrderByDescending(i => i.ItineraryId).ToListAsync();
        }

        public async Task<List<Itinerary>> SearchItinerary(string Name)
        {
            return await _dataContext.Itineraries.Include(c=>c.RouteCars).Include(c=>c.Cars).Where(n => n.RouteCars.OriginName.Contains(Name) || n.RouteCars.DestinationName.Contains(Name)).ToListAsync();
        }

        public async Task<string> ChangeIsuse(int Id, bool isuse)
        { 
            var checkIsuse = await _dataContext.Itineraries.FindAsync(Id);
            if (checkIsuse != null)
            {
                checkIsuse.isUse = isuse;
            }
            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";
            return null;
        }

    }
}
