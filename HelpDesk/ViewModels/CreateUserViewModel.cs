using System.ComponentModel.DataAnnotations;

namespace HelpDesk.ViewModels
{
    public class CreateUserViewModel {

        [Required]
        public string UserName { get; set; }= string.Empty;

        [Required]
        public string FullName { get; set; }= string.Empty;

        [EmailAddress]
        [Required]
        public string Email { get; set; } =string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Password is not matched")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}