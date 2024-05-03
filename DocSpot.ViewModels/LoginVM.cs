using System.ComponentModel.DataAnnotations;

namespace DocSpot.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Range(1, 4, ErrorMessage = "Please select account type")]
        public int AccountType { get; set; }
    }
}
