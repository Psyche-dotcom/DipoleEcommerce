using Ecommerce.Data.Context;
using Ecommerce.Data.Repository.Interface;
using Ecommerce.Model.DTO;
using Ecommerce.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data.Repository.Implementation
{
    public class CartRepo : ICartRepo
    {
        private readonly DipoleEcommerceContext _context;

        public CartRepo(DipoleEcommerceContext context)
        {
            _context = context;
        }

        public async Task<CartItem> AddProductToCart(CartItem cartItem)
        {
            var addcartItem = await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return addcartItem.Entity;
        }

        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            var addcart = await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return addcart.Entity;
        }

        public async Task<bool> DeleteCartAsync(Cart cart)
        {
            _context.Carts.Remove(cart);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveCartItemAsync(CartItem cart)
        {
            _context.CartItems.Remove(cart);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateCartItemAsync(CartItem cart)
        {
           _context.CartItems.Update(cart);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<CartItem> RetrieveUserCartItemAsync(string productid, string cartid)
        {
            var retrieveUserCart = await _context.CartItems.FirstOrDefaultAsync(c=> c.CartId == cartid && c.ProductId == productid );
            return retrieveUserCart;
        }
        public async Task<Cart> RetrieveUserCartAsync(string userid)
        {
            var retrieveUserCart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                    .ThenInclude(ci=>ci.Specification)
                .FirstOrDefaultAsync(u => u.UserId == userid);

            return retrieveUserCart;
        }
        public async Task<bool> ClearUserAllCartItems(string cartId)
        {
            var cartItems = _context.CartItems.Where(ci => ci.CartId == cartId);
            _context.CartItems.RemoveRange(cartItems);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;

        }
    }
}
