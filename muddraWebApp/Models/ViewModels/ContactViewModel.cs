using System.ComponentModel.DataAnnotations;
namespace muddraWebApp.Models.ViewModels;

public class ContactViewModel
{
    [Required(ErrorMessage = "*")]
    [Display(Name = "E-post")]
    [EmailAddress(ErrorMessage = "Ange en giltig E-post adress")]
    public string Email { get; set; } = null!;

    [MinLength(20, ErrorMessage = "Medelandet måste vara minst 20 bokstäver långt")]
    [Display(Name = "Medelande")]
    [Required(ErrorMessage = "*")]
    public string Message { get; set; } = null!;

    [Display(Name = "Adress / Område")]
    [Required(ErrorMessage = "*")]
    public string Area { get; set; } = null!;
}
