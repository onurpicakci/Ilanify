using System.ComponentModel.DataAnnotations;
using Ilanify.Domain.Enums;

namespace Ilanify.Domain.Entities;

public class RealEstate
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }
    
    [Required]
    [MaxLength(1500)]
    public string Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
     
    [Required]
    public int? SquareMeters { get; set; }
    
    public DateTime ListingDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public List<RealEstateImage> Images { get; set; }
    public RealEstateType Type { get; set; }
    public List<AttributeValue> AttributeValues { get; set; } 
    public bool IsActive { get; set; }
}