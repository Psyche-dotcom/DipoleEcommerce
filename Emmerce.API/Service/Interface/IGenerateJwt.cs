

using Ecommerce.Model.Entities;

namespace Ecommerce.API.Service.Interface
{
    public interface IGenerateJwt
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
