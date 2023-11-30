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
        private readonly ILogger<CartModel> _logger;
        public Models.Cart Cart { get; private set; }

        public CartModel(ApplicationDbContext context, ICartService cartService, ILogger<CartModel> logger)
        {
            _context = context;
            _cartService = cartService;
            _logger = logger;
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

        public   IActionResult OnPostRemoveItemFromCart(int removeProductId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var itemSneakerId = removeProductId;
                if (itemSneakerId == 0 || userId == null)
                {
                    _logger.LogError("Sneaker id was zero or the user id was null");
                    _logger.LogError(userId);
                    _logger.LogError(itemSneakerId.ToString());
                    return NotFound();
                }

                _cartService.RemoveItemFromCart(userId, itemSneakerId);

                return RedirectToPage();
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Could not remove Item from cart");
                return NotFound();
            }
        }
    }
}
