using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeneakerKop.Data;
using SeneakerKop.Models;

namespace SeneakerKop.Pages.snks
{
    public class EditModel : PageModel
    {
        private readonly SeneakerKop.Data.ApplicationDbContext _context;

        public EditModel(SeneakerKop.Data.ApplicationDbContext context)
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

            var sneaker =  await _context.Sneaker.FirstOrDefaultAsync(m => m.Id == id);
            if (sneaker == null)
            {
                return NotFound();
            }
            Sneaker = sneaker;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Sneaker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SneakerExists(Sneaker.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SneakerExists(int id)
        {
          return (_context.Sneaker?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
