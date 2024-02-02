namespace BookinKanAPI.DTOs.BookingCarsDTO
{
    public class RouteCarsDTO
    {
        public int RouteCarsId { get; set; }
        public  string OriginName { get; set; }
        public  string DestinationName { get; set; }
        public bool isUse { get; set; }
    }
}
