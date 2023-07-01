using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeneakerKop.Data;
using SeneakerKop.Models;

namespace SeneakerKop.Pages
{
    public class ProductDetailsModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        [FromQuery(Name ="id")]
        private int Id { get; set; }
        public int Quantity { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedQuantity { get; set; } = 1;

        public ProductDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Sneaker Sneaker { get; set; } = default!;
        public async Task<IActionResult> OnGet(int? id)


        {
            if (id == null || _context.Sneaker == null)
            {
                return NotFound();
            }

            var sneaker = await _context.Sneaker.FirstOrDefaultAsync(m => m.Id == id);
            if (sneaker == null)
            {
                return NotFound();
            }
            else
            {
                Sneaker = sneaker;
                Quantity  = Sneaker.Quantity;
            }
            return Page();
        }
    }
}
