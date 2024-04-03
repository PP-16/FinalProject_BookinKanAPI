using System.Text.Json.Serialization;

namespace BookinKanAPI.Models
{
    public class ImageCars
    {
        public int ImageCarsId { get; set; }
        public string Image { get; set; }

        public int CarsId { get; set; }

        [JsonIgnore]
        public Cars Cars { get; set; }
    }
}
