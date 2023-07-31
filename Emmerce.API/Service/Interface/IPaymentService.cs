using Ecommerce.Model.DTO;

namespace Ecommerce.API.Service.Interface
{
    public interface IPaymentService
    {
        Task<ResponseDto<TransactionData>> InitializePaymentAsync(string userid);
        Task<ResponseDto<string>> PaymentWebhookAsync(string status, string tx_ref, string transaction_id);
    }
}
