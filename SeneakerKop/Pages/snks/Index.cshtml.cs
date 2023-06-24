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
    public class IndexModel : PageModel
    {
        private readonly SeneakerKop.Data.ApplicationDbContext _context;

        public IndexModel(SeneakerKop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Sneaker> Sneaker { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Sneaker != null)
            {
                Sneaker = await _context.Sneaker.ToListAsync();
            }
        }
    }
}
