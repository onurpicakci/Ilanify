using Ilanify.Domain.Entities;
using Ilanify.Domain.Enums;

namespace Ilanify.Areas.Admin.Models.ViewModels;

public class AdminRealEstateEditViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public Category? Category { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int? SquareMeters { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Neighborhood { get; set; }
    public bool IsActive { get; set; }
    public RealEstateType Type { get; set; }
    public List<AttributeValueEditViewModel> AttributeValues { get; set; } = new List<AttributeValueEditViewModel>();
}