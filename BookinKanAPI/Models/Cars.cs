using System.Text.Json.Serialization;

namespace BookinKanAPI.Models
{
    public class Cars
    {
        public int CarsId { get; set; }
        public string CarRegistrationNumber { get; set; }
        public string CarModel { get; set; }
        public string CarBrand { get; set; }
        public string DetailCar { get; set; }

        public int QuantitySeat { get; set; }
        public int PriceSeat { get; set; }
        public bool isUse { get; set; }

        public CategoryCars CategoryCar { get; set; }
        public StatusCars StatusCar { get; set; }

        public int ClassCarsId { get; set; }
        public ClassCars ClassCars { get; set; }

        [JsonIgnore]                                                                              
        public List<ImageCars> ImageCars { get; set; }
    }
} 
