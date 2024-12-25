namespace BookinKanAPI.Models
{
    public class OrdersPastDue
    {
        public int OrdersPastDueId { get; set; }
        public DateTime RetrunDate { get; set; }
        public int NumberOfDays { get; set; }
        public int TotalPricePastDue { get; set; }
        public bool Paied { get; set; }

        public int OrderRentId { get; set; }
        public OrderRent OrderRent { get; set; }
        //public int OrderRentItemId { get; set; }
        //public OrderRentItem orderRentItem { get; set; }

    }
}
