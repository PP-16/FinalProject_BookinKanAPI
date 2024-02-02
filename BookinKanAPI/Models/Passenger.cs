namespace BookinKanAPI.Models
{
    public class Passenger
    {
        public int PassengerId { get; set; }
        public string PassengerName { get; set; }
        public string IDCardNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool isUse { get; set; }

        public int RoleId { get; set; }
        public Role Roles { get; set; }
    }
}
