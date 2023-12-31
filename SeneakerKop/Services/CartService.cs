﻿using Microsoft.EntityFrameworkCore;
using SeneakerKop.Data;
using SeneakerKop.Models;

namespace SeneakerKop.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CartService> _logger;

        public CartService(ApplicationDbContext dbContext, ILogger<CartService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Cart GetCartByUserId(string userId)
        {
            return _dbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.UserId == userId);
        }

        public void AddItemToCart(string userId, int sneakerId,string productName,double price, int quantity,string image,int availableStock)
        {
            var cart = GetOrCreateCart(userId);

            var cartItem = cart.CartItems.FirstOrDefault(item => item.SneakerId == sneakerId);

            if (cartItem == null)
            {
                cart.CartItems.Add(new CartItem { SneakerId = (int)sneakerId, Quantity = quantity,ProductName=productName,Price=price,Image=image,AvailableStock=availableStock});
            }
            else
            {
                cartItem.Quantity = quantity;
            }

            _dbContext.SaveChanges();
        }

        public void UpdateCartItemQuantity(string userId, int sneakerId, int quantity)
        {
            try
            {
                var cart = GetCartByUserId(userId);
                var cartItem = cart?.CartItems.FirstOrDefault(item => item.SneakerId == sneakerId);

                if (cartItem != null)
                {
                    cartItem.Quantity = quantity;
                    _dbContext.SaveChanges();
                }
                else
                {
                    // Log a message indicating that the cart item was not found
                    _logger.LogError($"Cart item with sneakerId {sneakerId} not found for user {userId}");
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during the update process
                _logger.LogError(ex, "Error updating cart item quantity");
                throw; // Rethrow the exception to maintain the flow
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




