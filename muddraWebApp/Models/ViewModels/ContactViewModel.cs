using System.ComponentModel.DataAnnotations;
namespace muddraWebApp.Models.ViewModels;

public class ContactViewModel
{
    [Required(ErrorMessage = "*")]
    [Display(Name = "E-post")]
    [EmailAddress(ErrorMessage = "Ange en giltig E-post adress")]
    public string Email { get; set; } = null!;

    [Display(Name = "Medelande")]
    [Required(ErrorMessage = "*")]
    public string Message { get; set; } = null!;

    [Display(Name = "Namn")]
    [Required(ErrorMessage = "*")]
    public string Name { get; set; } = null!;

    public string? LastName { get; set; }

    [Display(Name = "Adress / Område")]
    [Required(ErrorMessage = "*")]
    public string Area { get; set; } = null!;
}
