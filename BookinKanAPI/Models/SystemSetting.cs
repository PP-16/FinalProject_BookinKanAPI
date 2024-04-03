using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookinKanAPI.Models
{
    public class SystemSetting
    {
        [Key]
        public int SystemSettingId { get; set; }
        public string NameWeb { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ContactFB { get; set; }
        public string ContactIG { get; set; }
        public string ContactLine { get; set; }
        public string? Logo { get; set; } = null;
        public string? ImagePrompay { get; set; } = null;
        public List<ImageSlide> ImageSlide { get; set; } = new List<ImageSlide>();

    }

    public class ImageSlide
    {
        public int ImageSlideId { get; set; }
        public string ImageSlides { get; set; }

        public int SystemSettingId { get; set; }

        [JsonIgnore]
        public SystemSetting SystemSetting { get; set; }
    }
}
