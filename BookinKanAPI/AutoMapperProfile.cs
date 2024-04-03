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

            //CreateMap<Cars, CarsRequestDTO>();
            CreateMap<CarsRequestDTO, Cars>(); //.ForMember(dest => dest.ImageCars, opt => opt.MapFrom(src => src.ImageCars.Select(image => new ImageCars { Image = image.FileName })));
             
            CreateMap<Driver, DriverDTO>().ReverseMap();
            //CreateMap<DriverDTO, Driver>();

            CreateMap<OrderRent, OrderRentDTO>().ReverseMap();
            //CreateMap<OrderRentDTO, OrderRent>();

            CreateMap<OrderRentItem, OrderRentItemDTO>().ReverseMap();
            //CreateMap<OrderRentItemDTO, OrderRentItem>();

            //...............Booking.....................//

            CreateMap<Itinerary, ItineraryDTO>().ReverseMap();
            //CreateMap<ItineraryDTO, Itinerary>();

            CreateMap<RouteCars, RouteCarsDTO>().ReverseMap();
            //CreateMap<RouteCarsDTO, RouteCars>();

            CreateMap<Booking, BookingDTO>().ForMember(dest => dest.SeatNumbers, opt => opt.MapFrom(src => src.SeatsSerialized))
            .ReverseMap();
            CreateMap<BookingDTO, Booking>();

            CreateMap<Booking, EmployeeBookingDTO>().ForMember(dest => dest.SeatNumbers, opt => opt.MapFrom(src => src.SeatsSerialized))
            .ReverseMap();
            CreateMap<EmployeeBookingDTO, Booking>();


            CreateMap<PaymentBooking, PaymentDTO>().ReverseMap();
            //CreateMap<PaymentDTO, PaymentBooking>();

            CreateMap<SystemSetting, SystemSettingDTO>().ForMember(dest => dest.FormFiles, opt => opt.MapFrom(src => src.ImageSlide.Select(image => new ImageSlide { ImageSlides = image.ImageSlides }))).ReverseMap();
            //CreateMap<SystemSettingDTO, SystemSetting>();

            CreateMap<News, NewsDTO>().ForMember(dest => dest.FormFiles, opt => opt.MapFrom(src => src.ImageNews.Select(image => new ImageNews { Images = image.Images }))).ReverseMap();
            //CreateMap<NewsDTO, News>();

        }
    }
}
