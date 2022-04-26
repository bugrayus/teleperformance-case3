namespace teleperformance_case3.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public Category Category { get; set; }
}