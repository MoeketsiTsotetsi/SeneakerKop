using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeneakerKop.Data;
using SeneakerKop.Models;

namespace SeneakerKop.Pages.snks
{
    public class DeleteModel : PageModel
    {
        private readonly SeneakerKop.Data.ApplicationDbContext _context;

        public DeleteModel(SeneakerKop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Sneaker Sneaker { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Sneaker == null)
            {
                return NotFound();
            }
            var sneaker = await _context.Sneaker.FindAsync(id);

            if (sneaker != null)
            {
                Sneaker = sneaker;
                _context.Sneaker.Remove(Sneaker);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
