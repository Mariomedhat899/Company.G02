using System.ComponentModel.DataAnnotations;

namespace Company.G02.PL.DTOS
{
    public class ForgetPasswordDto
    {

        [Required(ErrorMessage = "Email is required")]

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
