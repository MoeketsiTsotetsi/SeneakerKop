using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SeneakerKop.Data;
using SeneakerKop.Models;
using SeneakerKop.Services;
using System.Security.Claims;

namespace SeneakerKop.Pages.Shop
{
    public class CartModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;
        public Models.Cart Cart { get; private set; }

        public CartModel(ApplicationDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public IActionResult OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
               return NotFound();
            }

            try
            {
                var serviceCart = _cartService.GetCartByUserId(userId);

                if (serviceCart != null)
                {
                    RedirectToPage("/Shop/Shop");

                }

                Cart = serviceCart;
                

                return Page();


            }
            catch (Exception ex)
            {
                // Log or handle any exceptions that might occur during cart retrieval
                Console.WriteLine($"Error retrieving cart: {ex.Message}");
                return NotFound();

            }
        }
    }
}
