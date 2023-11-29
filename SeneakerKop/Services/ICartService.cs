using SeneakerKop.Models;
using System.Collections.Generic;

namespace SeneakerKop.Services
{
    public interface ICartService
    {

        List<CartItem> GetCartItems();
        double GetTotalPrice();
        Cart GetCartByUserId(string userId);
        void AddItemToCart(string userId, int sneakerId,string ProductName,double Price, int quantity);
        void RemoveItemFromCart(string userId, int sneakerId);
    }
}
