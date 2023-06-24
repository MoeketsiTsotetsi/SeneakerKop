using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeneakerKop.Models;

namespace SeneakerKop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser>ApplicationUser { get; set; }

        public DbSet<SeneakerKop.Models.Sneaker>? Sneaker { get; set; }
    }
}
