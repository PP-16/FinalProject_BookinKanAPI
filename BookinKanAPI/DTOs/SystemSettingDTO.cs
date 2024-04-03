using BookinKanAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace BookinKanAPI.DTOs
{
    public class SystemSettingDTO
    {
        public int SystemSettingId { get; set; }
        public string NameWeb { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ContactFB { get; set; }
        public string ContactIG { get; set; }
        public string ContactLine { get; set; }
        public IFormFileCollection? Logo { get; set; }
        public IFormFileCollection? ImagePrompay { get; set; }
        public IFormFileCollection? FormFiles { get; set; }
    }
}
