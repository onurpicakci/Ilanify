using Ilanify.Domain.Enums;

namespace Ilanify.Domain.Entities;

public class Land : RealEstate
{
    public bool? IsInRegulation { get; set; }
    public string? ZoningStatus { get; set; } 
    
    public Land()
    {
        Type = RealEstateType.Land;
    } 
}