using System.ComponentModel.DataAnnotations;

namespace DishFinder.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        [Required,StringLength(100)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int? StallId { get; set; }

        public Stall? Stall { get; set; }
    }
}
