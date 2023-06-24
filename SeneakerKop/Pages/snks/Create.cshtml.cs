using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeneakerKop.Data;
using SeneakerKop.Models;

namespace SeneakerKop.Pages.snks
{
    public class CreateModel : PageModel
    {
        private readonly SeneakerKop.Data.ApplicationDbContext _context;

        public CreateModel(SeneakerKop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Sneaker Sneaker { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Sneaker == null || Sneaker == null)
            {
                return Page();
            }

            _context.Sneaker.Add(Sneaker);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
