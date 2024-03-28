namespace Ilanify.Domain.Entities;

public class AttributeValue
{
    public int AttributeValueId { get; set; }
    public int RealEstateId { get; set; }
    public int CategoryAttributeId { get; set; }
    public string Value { get; set; }
    public virtual RealEstate RealEstate { get; set; }
    public virtual CategoryAttribute CategoryAttribute { get; set; }
}