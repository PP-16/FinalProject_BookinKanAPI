
using System.Text.Json.Serialization;

namespace BookinKanAPI.Models
{
    public class ImageNews
    {

            public int ImageNewsId { get; set; }
            public string Images { get; set; }

            public int NewsId { get; set; }

            [JsonIgnore]
            public News News { get; set; }
    }
}
