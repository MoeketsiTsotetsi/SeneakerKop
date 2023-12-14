using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;

namespace SeneakerKop.Pages.Shop
{
    public class SuccessModel : PageModel
    {

        private readonly ILogger<SuccessModel> _logger;

        public SuccessModel(ILogger<SuccessModel> logger)
        {
          _logger = logger;
        }
        public void OnGet()
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());
            _logger.LogInformation("***************Success page ran**************");

            _logger.LogInformation("Email:" + session.CustomerEmail);
            _logger.LogInformation("Payment:" + session.PaymentStatus);
            
        }
    }
}
