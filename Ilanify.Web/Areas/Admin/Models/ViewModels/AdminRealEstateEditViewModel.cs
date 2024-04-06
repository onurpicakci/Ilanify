using Ilanify.Domain.Entities;

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
}