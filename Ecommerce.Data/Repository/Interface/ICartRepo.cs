using Ecommerce.Model.DTO;
using Ecommerce.Model.Entities;

namespace Ecommerce.Data.Repository.Interface
{
    public interface ICartRepo
    {
        Task<Cart> CreateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(Cart cart);
        Task<Cart> RetrieveUserCartAsync(string userid);
        Task<CartItem> AddProductToCart(CartItem cartItem);
        Task<bool> ClearUserAllCartItems(string cartId);
        Task<bool> RemoveCartItemAsync(CartItem cart);
        Task<CartItem> RetrieveUserCartItemAsync(string productid, string cartid);
        Task<bool> UpdateCartItemAsync(CartItem cart);

    }
}
