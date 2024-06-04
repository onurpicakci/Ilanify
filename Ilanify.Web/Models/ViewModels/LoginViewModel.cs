using System.ComponentModel.DataAnnotations;

namespace Ilanify.Models.ViewModels;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Kullanıcı Adı")]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; }

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
}