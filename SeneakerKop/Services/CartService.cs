using Microsoft.EntityFrameworkCore;
using SeneakerKop.Data;
using SeneakerKop.Models;

namespace SeneakerKop.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _dbContext;

        public CartService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Cart GetCartByUserId(string userId)
        {
            return _dbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.UserId == userId);
        }

        public void AddItemToCart(string userId, int sneakerId,string productName,double price, int quantity)
        {
            var cart = GetOrCreateCart(userId);

            var cartItem = cart.CartItems.FirstOrDefault(item => item.SneakerId == sneakerId);

            if (cartItem == null)
            {
                cart.CartItems.Add(new CartItem { SneakerId = (int)sneakerId, Quantity = quantity,ProductName=productName,Price=price});
            }
            else
            {
                cartItem.Quantity = quantity;
            }

            _dbContext.SaveChanges();
        }

        public void UpdateCartItemQuantity(string userId, int sneakerId, int quantity)
        {
            var cart = GetCartByUserId(userId);
            var cartItem = cart?.CartItems.FirstOrDefault(item => item.SneakerId == sneakerId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _dbContext.SaveChanges();
            }
        }

        public void RemoveItemFromCart(string userId, int sneakerId)
        {
            var cart = GetCartByUserId(userId);
            var cartItem = cart?.CartItems.FirstOrDefault(item => item.SneakerId == sneakerId);

            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);
                _dbContext.SaveChanges();
            }
        }

        public List<CartItem> GetCartItems(string userId)
        {
            var cart = GetCartByUserId(userId);
            return cart?.CartItems.ToList() ?? new List<CartItem>();
        }

        // Helper method to get or create a cart for a user
        private Cart GetOrCreateCart(string userId)
        {
            var cart = GetCartByUserId(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId, CartItems = new List<CartItem>() };
                _dbContext.Carts.Add(cart);
                _dbContext.SaveChanges();
            }
            return cart;
        }

        public List<CartItem> GetCartItems()
        {
            throw new NotImplementedException();
        }

        public double GetTotalPrice()
        {
            throw new NotImplementedException();
        }

       
    }



}




