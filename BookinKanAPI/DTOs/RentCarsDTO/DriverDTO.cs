namespace BookinKanAPI.DTOs.RentCarsDTO
{
    public class DriverDTO
    {
        public int? DriverId { get; set; }
        public  string DriverName { get; set; }
        public  string IDCardNumber { get; set; }
        public  double Charges { get; set; }
        public  string Address { get; set; }
        public  string Phone { get; set; }
        public  int StatusDriver { get; set; }
        public bool isUse { get; set; }
    }
}
