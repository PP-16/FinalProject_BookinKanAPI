namespace BookinKanAPI.DTOs.AuthenDto
{
    public class UserDTO
    {
        public int PassengerId { get; set; }
        public string PassengerName { get; set; }
        public string IDCardNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public string? ImagePassenger { get; set; }
        public string Token { get; set; }
        public bool isUse { get; set; }
    }
    public class UploadImageDTO
    {
        public int PassengerId { get; set; }
        public IFormFileCollection? ImagePassenger { get; set; }
    }
}
