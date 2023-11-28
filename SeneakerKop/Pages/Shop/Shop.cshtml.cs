using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SeneakerKop.Data;
using SeneakerKop.Models;
using SeneakerKop.Services;

public class ShopModel : PageModel
{
    
    private readonly ApplicationDbContext _dbContext;

    public ShopModel(ICartService cartService, ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Sneaker> Sneakers { get; set; } = new List<Sneaker>(); // Initialize the list

    public void OnGet()
    {
        try
        {
            Sneakers = _dbContext.Sneaker.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving sneakers: {ex.Message}");
        
        }
    }

    
}
