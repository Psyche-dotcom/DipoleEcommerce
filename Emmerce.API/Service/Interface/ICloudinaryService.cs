using CloudinaryDotNet.Actions;

namespace Ecommerce.API.Service.Interface
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadPhoto(IFormFile file, object id);
    }
}
