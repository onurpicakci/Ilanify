namespace Ilanify.Domain.Entities;

public class RealEstateImage
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public RealEstate RealEstate { get; set; }
    public int RealEstateId { get; set; }
}