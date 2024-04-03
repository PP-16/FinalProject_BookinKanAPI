using BookinKanAPI.Models;

namespace BookinKanAPI.DTOs
{
    public class NewsDTO
    {
        public int? NewsId { get; set; }

        public string NewsName { get; set; }
        public string NewsDetails { get; set; }
        public IFormFileCollection? FormFiles { get; set; }
    }
}
