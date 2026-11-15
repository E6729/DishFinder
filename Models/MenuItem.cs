namespace DishFinder.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }
        public string? description { get; set; }
        public string StallId { get; set; }

        public Stall? Stall { get; set; }
    }
}
