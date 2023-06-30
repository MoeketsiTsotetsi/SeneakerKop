using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SeneakerKop.Data;
using SeneakerKop.Models;

namespace SeneakerKop.Pages
{
    public class ShopModel : PageModel
    {
        
        private readonly ApplicationDbContext _context;
        public List<Sneaker> Sneakers { get; set; }

        public ShopModel(ApplicationDbContext context)
        {
           _context = context;
        }
        public void OnGet()
        {
           Sneakers = _context.Sneaker.ToList();
        }
    }
}
