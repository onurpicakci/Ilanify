namespace Ilanify.Domain.Entities;

public class Location
{
    public int Id { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Neighboord { get; set; }
    public List<RealEstate> RealEstates { get; set; }
}