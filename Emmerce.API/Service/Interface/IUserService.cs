using Ecommerce.Model.DTO;
using Model.DTO;

namespace Emmerce.API.Service.Interface
{
    public interface IUserService
    {
        Task<ResponseDto<string>> RegisterUser(SignUp signUp, string Role);
        Task<ResponseDto<string>> LoginUser(SignInModel signIn);
        //Task<ResponseDto<string>> LogoutUser(string UserEmail);
        Task<ResponseDto<string>> UpdateUser(string email, UpdateUserDto updateUser);
        Task<ResponseDto<string>> ForgotPassword(string UserEmail);
        Task<ResponseDto<string>> ResetUserPassword(ResetPasswordDto resetPassword);
        Task<ResponseDto<string>> DeleteUser(string email);
        //Task<ResponseDto<string>> UploadUserProfilePicture(string email, IFormFile file);
        Task<ResponseDto<string>> UpdateUserRole(string email, string role);
    }
}
