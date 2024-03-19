using System.ComponentModel.DataAnnotations;

namespace Ilanify.Domain.Entities;

public class Location
{
    public int Id { get; set; }
    
    [Required]
    public string City { get; set; }
    
    [Required]
    public string District { get; set; }
    
    [Required]
    public string Neighboord { get; set; }
    public List<RealEstate> RealEstates { get; set; }
}