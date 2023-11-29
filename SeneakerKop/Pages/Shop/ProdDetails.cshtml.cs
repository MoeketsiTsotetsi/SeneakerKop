using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using SeneakerKop.Data;
using SeneakerKop.Models;
using SeneakerKop.Services;
using System.Security.Claims;

namespace SeneakerKop.Pages
{
    public class ProductDetailsModel : PageModel

    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;
        private readonly ILogger<ProductDetailsModel> _logger;
        
        public int Quantity { get; private set; }

        [BindProperty]
        public int SelectedQuantity { get; set; } = 1;
        public Sneaker Sneaker { get; private set; } = default!;
        public string CompletionMessage { get; private set; }

        public ProductDetailsModel(
            ApplicationDbContext context,
            ICartService cartService,
            ILogger<ProductDetailsModel> logger)
        {
            _context = context;
            _cartService = cartService;
            _logger = logger;
            
        }

        public async Task<IActionResult> OnGetAsync(int? sneakerId)
        {
            if (sneakerId == null)
            {
                _logger.LogError("No ID provided for Sneaker details retrieval.");
                return NotFound();
            }

            var sneaker = await _context.Sneaker.FirstOrDefaultAsync(m => m.Id == sneakerId);
            if (sneaker == null)
            {
                _logger.LogError($"Sneaker with ID {sneakerId} not found.");
                return NotFound();
            }
            else
            {
                Sneaker = sneaker;
                Quantity = Sneaker.Quantity;
            }
            if (TempData.ContainsKey("CompletionMessage"))
            {
                CompletionMessage = TempData["CompletionMessage"] as string;
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAddToCart(int sneakerId)
        {
            try
            {
                var selectedSneaker = await _context.Sneaker.FirstOrDefaultAsync(s => s.Id == sneakerId);
                var saleQuantity = SelectedQuantity;

                if (selectedSneaker == null)
                {
                    _logger.LogError("Selected sneaker not found.");
                    return NotFound();
                }

                if (!User.Identity.IsAuthenticated)
                {
                    // Redirect unauthenticated users to the login page
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }

                // If the user is authenticated, get the user ID
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var ProductName = selectedSneaker.Name;
                var Price = selectedSneaker.Price;


                // Use userId in your logic or store it in the cart
                _cartService.AddItemToCart(userId, sneakerId, ProductName, Price, saleQuantity);
               
                TempData["CompletionMessage"] = "Item(s) added to cart successfully!";
                return RedirectToPage("/Shop/ProdDetails", new { sneakerId });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your requirement
                _logger.LogError(ex, "An error occurred while processing the request.");

                // You can return an error page or a specific error message to the user
                return RedirectToPage("/Error");
            }
        }


    }
}
