using Microsoft.EntityFrameworkCore;

namespace DishFinder.Models
{
    public class DishFinderDbContext : DbContext
    {
        public DbSet<DishFinder.Models.OrderItem> OrderItem { get; set; } = default!;
        public DishFinderDbContext(DbContextOptions<DishFinderDbContext> options) : base(options) { }
        public DbSet<Stall> Stalls { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MenuItem>().Property(m => m.Price).HasPrecision(18, 2);

            //sample data
            modelBuilder.Entity<Stall>().HasData(
                new Stall { Id = 1, Name = "Taste of Lagoon", Description = "Filipino Breakfast Favorites!", Location = "Stall #1" },
                new Stall { Id = 2, Name = "Unang Kagat Tinapay Agad", Description = "Tasty Burgers and Sandwiches", Location = "Stall #2" }
                );

            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem { Id = 1, Name = "Giant Burger", Price = 40, Description = "Big ahh Burger", StallId = 2 },
                new MenuItem { Id = 2, Name = "Tosilog", Price = 50, Description = "A classic Filipino especialty", StallId = 1 }
                );
        }
    }
}
