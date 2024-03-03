using Ilanify.Domain.Enums;

namespace Ilanify.Domain.Entities;

public class House : RealEstate
{
    public int? NumberOfRooms { get; set; }
    public int? NumberOfBathrooms { get; set; }
    public int? NumberOfFloors { get; set; }
    public bool? HasGarden { get; set; }
    public bool? HasGarage { get; set; }
    public bool? IsFurnished { get; set; }
    public string? HeatingType { get; set; }
    public string? HasBalcony { get; set; }
    public string? YearBuilt { get; set; }
 
    public House()
    {
        Type = RealEstateType.House;
    }
}