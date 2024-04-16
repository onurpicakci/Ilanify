using Microsoft.AspNetCore.Identity;

namespace Ilanify.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ProfileImageUrl { get; set; }
    public List<RealEstate> RealEstates { get; set; }
}