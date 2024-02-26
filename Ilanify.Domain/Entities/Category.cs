namespace Ilanify.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<RealEstate> RealEstates { get; set; }
}