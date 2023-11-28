using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeneakerKop.Data;
using SeneakerKop.Models;
using SeneakerKop.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SeneakerKop.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;
        private readonly ILogger<ProductDetailsModel> _logger;

        public int Quantity { get; private set; }

        public int SelectedQuantity { get; set; } = 1;
        public Sneaker Sneaker { get; private set; } = default!;

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
            return Page();
        }


        [Authorize]
        public IActionResult OnPostAddToCart(int sneakerId)
        {
            var selectedSneaker = _context.Sneaker.FirstOrDefault(s => s.Id == sneakerId);
            var saleQuantity = SelectedQuantity;

            if (selectedSneaker == null)
            {
                _logger.LogError("Selected sneaker not found.");
                return NotFound();
            }

            if (!User.Identity.IsAuthenticated)
            {
                // Redirect or handle the case where user is not logged in
                // For example, redirect to the login page
                return RedirectToPage("/Account/Login", new { area = "Identity" });


            }


            _cartService.AddItemToCart("testid", sneakerId, saleQuantity);
             return new JsonResult(new { SneakerName = selectedSneaker.Name, QuantityAdded = Quantity });
            

            
        }
    }
}
