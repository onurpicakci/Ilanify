namespace Ilanify.Models.ViewModels;

public class AttributeValueEditViewModel
{
    public int Id { get; set; }
    public string Value { get; set; }
    public string? CategoryAttributeName { get; set; }
    public int CategoryAttributeId { get; set; }
    public int RealEstateId { get; set; }
}