using System.ComponentModel.DataAnnotations;

namespace Ilanify.Models.ViewModels;

public class ProfileViewModel
{
    [Required] 
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.PhoneNumber)]
    [Phone]
    public string PhoneNumber { get; set; }
    
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
    public string? ConfirmPassword { get; set; }
    public string? ProfileImageUrl { get; set; }
    public IFormFile? ProfileImage { get; set; }
    

}