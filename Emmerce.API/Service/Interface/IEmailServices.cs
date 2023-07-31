

using Ecommerce.Model.DTO;

namespace Ecommerce.API.Service.Interface
{
    public interface IEmailServices
    {
        void SendEmail(Message message);
    }
}
