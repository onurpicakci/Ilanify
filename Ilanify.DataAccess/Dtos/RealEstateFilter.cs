namespace Ilanify.DataAccess.Dtos;

public class RealEstateFilter
{
    public string? City { get; set; }
    public int? CategoryId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinSquareMeters { get; set; }
    public int? MaxSquareMeters { get; set; }
    public int? MinRooms { get; set; }
    public int? MaxRooms { get; set; }
}