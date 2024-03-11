using System.ComponentModel.DataAnnotations.Schema;

namespace Ilanify.Domain.Entities;

public class CategoryAttribute
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public virtual Category Category { get; set; }
    public List<AttributeValue> AttributeValues { get; set; } 
}