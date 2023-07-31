using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class SignInModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
