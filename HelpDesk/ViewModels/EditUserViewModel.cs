using System.ComponentModel.DataAnnotations;

namespace HelpDesk.ViewModels
{
    public class EditUserViewModel {

        public string? Id { get; set; }
        public string? FullName { get; set; }

        [EmailAddress]
        public string? Email { get; set; } 

        [DataType(DataType.Password)]
        public string? Password { get; set; } 

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password is not matched")]
        public string? ConfirmPassword { get; set; }
        public IList<string>? SelectedRoles { get; set; }
    }
}