using System.ComponentModel.DataAnnotations;

namespace Ilanify.Models.ViewModels;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Adı")]
    public string FirstName { get; set; }
    
    [Required]
    [Display(Name = "Soyadı")]
    public string LastName { get; set; }
    
    [Required]
    [Display(Name = "Kullanıcı Adı")]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Şifre Tekrar")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
    
    [Required]
    [Display(Name = "Telefon Numarası")]
    public string PhoneNumber { get; set; }
}