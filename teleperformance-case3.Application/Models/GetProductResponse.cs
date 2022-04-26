namespace teleperformance_case3.Application.Models;

public class GetProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public GetCategoryResponse Category { get; set; }
}