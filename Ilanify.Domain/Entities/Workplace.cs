using Ilanify.Domain.Enums;

namespace Ilanify.Domain.Entities;

public class Workplace : RealEstate
{
    public bool? HasParking { get; set; }
    public bool? HasSecurity { get; set; }
    public bool? HasMeetingRooms { get; set; }
    public bool? HasKitchenFacilities { get; set; }
    public bool? HasParkingSpace { get; set; }
    public bool? HasElevator { get; set; }
    public string? BusinessType { get; set; }
    
    public Workplace()
    {
        Type = RealEstateType.Workplace;
    }
}