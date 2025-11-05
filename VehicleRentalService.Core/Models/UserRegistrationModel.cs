using System.ComponentModel.DataAnnotations;

namespace VehicleRentalService.Models
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^[A-Za-zА-Яа-яЁёІіЇїЄє'\s-]+$", ErrorMessage = "First name can contain only letters and spaces")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(@"^[A-Za-zА-Яа-яЁёІіЇїЄє'\s-]+$", ErrorMessage = "Last name can contain only letters and spaces")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
    ErrorMessage = "Password must be at least 8 characters long and contain uppercase, lowercase letters, and a number.")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
