namespace DishFinder.Models
{
    public class Stall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Location { get; set; }

        public List<MenuItem> MenuItems { get; set; } = new();
    }
}
    