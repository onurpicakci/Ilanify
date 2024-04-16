namespace Ilanify.Domain.Entities;

public class Favorites
{
    public int Id { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public string ApplicationUserId { get; set; }
    public RealEstate RealEstate { get; set; }
    public int RealEstateId { get; set; }
}