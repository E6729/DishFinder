using DishFinder.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;

namespace DishFinder.Controllers
{
    public class CartController : Controller
    {
        private readonly DishFinderDbContext _context;

        public CartController(DishFinderDbContext context)
        {
            _context = context;
        }

        private List<CartItem> GetCart()
        {
            var json = HttpContext.Session.GetString("Cart");
            // TODO: if json is null, return a new empty list
            if(json == null)
            {
                return new List<CartItem>();
            }
            else
            {
                return JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
            }
            // TODO: otherwise turn the JSON back into a List<CartItem>
            //       (hint: JsonSerializer.Deserialize<List<CartItem>>)
        }

        private void SaveCart(List<CartItem> cart)
        {
            // TODO: turn the list into a JSON string (JsonSerializer.Serialize)
            var json = JsonSerializer.Serialize(cart);
            //       and store it with HttpContext.Session.SetString("Cart", ...)
            HttpContext.Session.SetString("Cart", json);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int MenuItemId)
        {
            // 1. Find the dish in the database (look at how StallsController.Details does it)
            var item = await _context.MenuItems.FindAsync(MenuItemId);
            //    What should happen if it's not found?
            if(item == null)
            {
                return NotFound();
            }
            // 2. Get the cart
            var cart = GetCart();
            // 3. Is this dish already in the cart?
            var ExistingItem = cart.FirstOrDefault(c => c.MenuItemId == MenuItemId);
            if(ExistingItem != null)
            {
                ExistingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    MenuItemId = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = 1
                });

            }
            //    yes -> increase its Quantity
            //    no  -> add a new CartItem built from the dish
            // 4. Save the cart
            SaveCart(cart);
            // 5. Redirect to the cart's Index action
            return RedirectToAction(nameof(Cart));
        }

        public IActionResult Cart()
        {
            // we'll do this after Add works; for now it can
            return View(GetCart());
        }

        public async Task<IActionResult> Delete(int? MenuItemId)
        {
            if (MenuItemId == null)
            {
                return NotFound();
            }

            var cart = GetCart();

            var ItemToRemove = cart.FirstOrDefault(c => c.MenuItemId == MenuItemId);
            if (ItemToRemove != null)
            {
                cart.Remove(ItemToRemove);
                SaveCart(cart);
            }

            return RedirectToAction(nameof(Cart));
        }
    }
}
