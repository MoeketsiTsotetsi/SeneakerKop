using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SeneakerKop.Data;
using SeneakerKop.Models;
using SeneakerKop.Services;
using Stripe.Checkout;
using System.Security.Claims;

namespace SeneakerKop.Pages.Pay
{
    public class CheckoutModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CheckoutModel> _logger;
        private readonly ICartService _cartService;
        private readonly IConfiguration _configuration;
        public Cart Cart { get; set; }

        public CheckoutModel(ApplicationDbContext context, ILogger<CheckoutModel> logger, ICartService cartService,IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _cartService = cartService;
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _logger.LogInformation("On Get Ran");

                if (string.IsNullOrEmpty(userId))
                {
                    return NotFound();
                }

                var serviceCart = _cartService.GetCartByUserId(userId);

                if (serviceCart == null)
                {
                    return RedirectToPage("/Shop/Shop");
                }

                _logger.LogInformation("Cart not null");
                Cart = serviceCart;
                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving cart: {ex.Message}");
                return NotFound();
            }
        }


        public ActionResult OnPostCheckout(Cart cart) {
            
            try {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var _cart = _cartService.GetCartByUserId(userId);
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var domain = "https://localhost:7084/";

                if (cart == null)
                {
                   return RedirectToPage("/Pay/Cart");
                }

                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + $"Shop/Success",
                    CancelUrl = domain + "Shop/Cart",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    CustomerEmail = userEmail

                };

                foreach (var item in _cart.CartItems)
                {
                    var sessionListItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100),
                            Currency = "zar",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.ProductName.ToString(),
                            }

                        },
                        Quantity = item.Quantity,
                    };

                    options.LineItems.Add(sessionListItem);
                }


                var service = new SessionService();
                Session session = service.Create(options);
                TempData["Session"] = session.Id;

                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Errro: {ex.Message}");
                return NotFound();
            }
        }


     
    }

}

