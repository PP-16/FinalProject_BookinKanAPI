using AutoMapper;
using BookinKanAPI.DTOs;
using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.Models;

namespace BookinKanAPI
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ClassCars, ClassCarsDTO>();
            CreateMap<ClassCarsDTO, ClassCars>().ForMember(dest => dest.ClassCarsId, opt => opt.Ignore()); ;

            CreateMap<Cars, CarsRequestDTO>();
            CreateMap<CarsRequestDTO, Cars>().ForMember(dest => dest.ImageCars, opt => opt.MapFrom(src => src.ImageCars.Select(image => new ImageCars { Image = image.FileName }))); 

            CreateMap<Driver, DriverDTO>();
            CreateMap<DriverDTO, Driver>();

            CreateMap<OrderRent, OrderRentDTO>();
            CreateMap<OrderRentDTO, OrderRent>();

            CreateMap<OrderRentItem, OrderRentItemDTO>();
            CreateMap<OrderRentItemDTO, OrderRentItem>();

            //...............Booking.....................//

            CreateMap<Itinerary, ItineraryDTO>();
            CreateMap<ItineraryDTO, Itinerary>();

            CreateMap<RouteCars, RouteCarsDTO>();
            CreateMap<RouteCarsDTO, RouteCars>();

            CreateMap<Booking, BookingDTO>().ForMember(dest => dest.SeatNumbers, opt => opt.MapFrom(src => src.SeatsSerialized))
            .ReverseMap();
            CreateMap<BookingDTO, Booking>();

        }
    }
}
