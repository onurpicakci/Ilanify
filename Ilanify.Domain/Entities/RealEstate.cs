using Ilanify.Domain.Enums;

namespace Ilanify.Domain.Entities;

public abstract class RealEstate
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    
    public int? SquareMeters { get; set; }
    public DateTime ListingDate { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public List<RealEstateImage> Images { get; set; }
    public RealEstateType Type { get; set; }
}

