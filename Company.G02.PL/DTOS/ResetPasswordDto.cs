using System.ComponentModel.DataAnnotations;

namespace Company.G02.PL.DTOS
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password Is Required !")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).{8,}$", ErrorMessage = "Password Must Be Min 8, Uppercase, Lowercase, Digit, Special Character")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmPassword Is Required !")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Password And Confirm Password Not Match !")]
        public string ConfirmPassword { get; set; }
    }
}
