using System.ComponentModel.DataAnnotations;

namespace Company.G02.PL.DTOS
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "UserName Is Required !")]
        public string Username { get; set; }

        [Required(ErrorMessage = "First Name Is Required !")]

        public string FName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required !")]

        public string LName { get; set; }

        [Required(ErrorMessage = "Password Is Required !")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).{8,}$",ErrorMessage = "Password Must Be Min 8, Uppercase, Lowercase, Digit, Special Character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword Is Required !")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password And Confirm Password Not Match !")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }

        [Required(ErrorMessage = "Email Is Required !")]

        [EmailAddress(ErrorMessage = "Invalid Email Address !")]

        public string Email { get; set; }
    }
}
