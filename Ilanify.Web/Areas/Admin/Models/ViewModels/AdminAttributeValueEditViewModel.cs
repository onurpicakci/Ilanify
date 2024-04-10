namespace Ilanify.Areas.Admin.Models.ViewModels;

public class AdminAttributeValueEditViewModel
{
    public int Id { get; set; }
    public string Value { get; set; }
    public string? CategoryAttributeName { get; set; }
    public int CategoryAttributeId { get; set; }
    public int RealEstateId { get; set; }
}