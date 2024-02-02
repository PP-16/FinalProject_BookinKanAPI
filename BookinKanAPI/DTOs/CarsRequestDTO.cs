using BookinKanAPI.Models;
using System.ComponentModel;

namespace BookinKanAPI.DTOs
{
    public class CarsRequestDTO
    {
        public int? CarsId { get; set; }
        public  string CarRegistrationNumber { get; set; }
        public  string CarModel { get; set; }
        public  string CarBrand { get; set; }
        public  string DetailCar { get; set; }
        public  int CategoryCar { get; set; }
        public  int StatusCar { get; set; } = 0;
        public  int ClasscarsId { get; set; }
        public int QuantitySeat { get; set; }
        public int PriceSeat { get; set; }
        public IFormFileCollection? ImageCars { get; set; }
        public bool isUse { get; set; }
    }
}
