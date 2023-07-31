using Ecommerce.Model.DTO;
using Ecommerce.Model.Entities;

namespace Ecommerce.API.Service.Interface
{
    public interface IOrderService
    {
        Task<ResponseDto<string>> CreateOrderAsync(AddOrderDto addOrder);
       
        Task<ResponseDto<DisplayOrderDto>> GetActiveUserOrderbyUserIdAsync(string userid);
        
        Task<ResponseDto<string>> RemoveActiveUserOrderbyUserIdAsync(string userid);
    }
}
