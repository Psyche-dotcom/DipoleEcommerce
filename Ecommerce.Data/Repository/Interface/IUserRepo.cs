using Ecommerce.Model.DTO;
using Ecommerce.Model.Entities;

namespace Ecommerce.Data.Repository.Interface
{
    public interface IUserRepo
    {
        Task<ApplicationUser> SignUpAsync(ApplicationUser user, string Password);
        Task<bool> CheckAccountPassword(ApplicationUser user, string password);
        Task<ResetPasswordDto> ResetPasswordAsync(ApplicationUser user, ResetPasswordDto resetPassword);
        Task<bool> CheckEmailConfirmed(ApplicationUser user);
        Task<bool> AddRoleAsync(ApplicationUser user, string Role);
        Task<string> ForgotPassword(ApplicationUser user);
        Task<bool> ConfirmEmail(string token, ApplicationUser user);
        Task<bool> RemoveRoleAsync(ApplicationUser user, IList<string> role);
        Task<bool> RoleExist(string Role);
        Task<ApplicationUser?> FindUserByEmailAsync(string email);
        Task<ApplicationUser> FindUserByIdAsync(string id);
        Task<bool> UpdateUserInfo(ApplicationUser applicationUser);
        Task<IList<string>> GetUserRoles(ApplicationUser user);
        
        Task<bool> DeleteUserByEmail(ApplicationUser user);
    }
}
