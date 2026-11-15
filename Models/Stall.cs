using System.ComponentModel.DataAnnotations;

namespace DishFinder.Models
{
    public class Stall
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required, StringLength(50)]
        public string Location { get; set; }

        public List<MenuItem> MenuItems { get; set; } = new();
    }
}
    