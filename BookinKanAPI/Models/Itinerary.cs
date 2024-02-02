namespace BookinKanAPI.Models
{
    public class Itinerary
    {
        public int ItineraryId { get; set; }
        public DateTime IssueTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool isUse { get; set; }
        public int RouteCarsId { get; set; }
        public RouteCars RouteCars { get; set; }

        public int CarsId { get; set; }
        public Cars Cars { get; set; }

    }
}
