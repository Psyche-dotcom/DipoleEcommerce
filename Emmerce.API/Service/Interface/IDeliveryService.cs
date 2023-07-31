using Ecommerce.Model.DTO;

namespace Ecommerce.API.Service.Interface
{
    public interface IDeliveryService
    {
        Task<ResponseDto<string>> CreateDeliveryAddressAsync(AddDeliveryDto deliveryDto, string userid);
        Task<ResponseDto<DisplayDeliveryAddressDto>> GetDeliveryAddressAsync(string userid);
        Task<ResponseDto<DisplayDeliveryCharges>> GetShippingfeeQoute(string userid);
        Task<ResponseDto<string>> UpdateDeliveryAddressAsync(UpdateDeliveryDto deliveryDto, string userid);
    }
}
