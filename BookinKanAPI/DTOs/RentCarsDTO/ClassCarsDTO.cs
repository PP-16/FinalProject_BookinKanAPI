namespace BookinKanAPI.DTOs.RentCarsDTO
{
    public class ClassCarsDTO
    {
        public int ClassCarsId { get; set; }
        public required string ClassName { get; set; }
        public required int Price { get; set; }
        public bool isUse { get; set; }
    }
}
