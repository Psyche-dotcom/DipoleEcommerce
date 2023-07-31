using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.DTO
{
    public class ForgotPasswordDto
    {
        public string Email { get; set; }
        public string ResetToken { get; set; }
        public ForgotPasswordDto(string email, string resetToken)
        {
            Email = email;
            ResetToken = resetToken;
             
        }
    }
}
