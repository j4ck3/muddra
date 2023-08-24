using System.ComponentModel.DataAnnotations;

namespace muddraWebApp.Models.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Ange din E-post adress")]
        [Display(Name = "E-post")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;


        [Required(ErrorMessage = "Ange ditt lösenord")]
        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
