using Ilanify.Domain.Enums;

namespace Ilanify.DataAccess.Dtos;

public class RealEstateFilter
{
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Neighborhood { get; set; }
    public int? CategoryId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinSquareMeters { get; set; }
    public int? MaxSquareMeters { get; set; }
    public string? RoomCount { get; set; }
    public RealEstateType? RealEstateType { get; set; }
}