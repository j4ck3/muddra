using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace muddraWebApp.Models.ViewModels;

public class SignUpViewModel
{
    [Required(ErrorMessage = "Du måste ange en giltig E-post adress")]
    [Display(Name = "E-post")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;


    [Display(Name = "Telefonnummer (Valfritt)")]
    public string? PhoneNumber { get; set; }



    [Required(ErrorMessage = "Enge ett lösenord")]
    [MinLength(8, ErrorMessage = "Enge ett lösenord")]
    [Display(Name = "Lösenord")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;




    [Required(ErrorMessage = "Bekräfta ditt lösenord.")]
    [Display(Name = "Bekräfta ditt lösenord")]
    [Compare("Password", ErrorMessage = "Lösenorden matchar inte.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;

    public static implicit operator IdentityUser(SignUpViewModel model)
    {
        return new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
        };
    }
}
    

