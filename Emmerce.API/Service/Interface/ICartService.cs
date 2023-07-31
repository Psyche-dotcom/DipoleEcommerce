using Ecommerce.Model.DTO;
using Ecommerce.Model.Entities;

namespace Ecommerce.API.Service.Interface
{
    public interface ICartService
    {
        Task<ResponseDto<DisplayCart>> CreateCartAsync(string userid);
        Task<ResponseDto<CartItem>> AddCartItemAsync(AddCartItemDto CartItem);
        Task<ResponseDto<DisplayCart>> GetCartbyUserIdAsync(string userid);
        Task<ResponseDto<DisplayCart>> UpdateUserCartItemAsync(string userid, string productid, int quantity);
        Task<ResponseDto<string>> RemoveUserCartItemAsync(string userid, string productid);
    }
}
