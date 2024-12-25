using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookinKanAPI.Models
{
    public class News
    {

        public int NewsId { get; set; }

        public string NewsName { get; set; }
        public string NewsDetails { get; set; }

        public DateTime? CreateAt { get; set; }
        public ICollection<ImageNews> ImageNews { get; set; } = new List<ImageNews>();
    }

}
