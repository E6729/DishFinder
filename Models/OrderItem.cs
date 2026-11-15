using System.ComponentModel.DataAnnotations;

namespace DishFinder.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }

        [Range (1, 100)]
        public int Quantity { get; set; }
    }
}
