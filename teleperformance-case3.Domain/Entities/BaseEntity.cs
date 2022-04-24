using System.ComponentModel.DataAnnotations;

namespace teleperformance_case3.Domain.Entities;

public class BaseEntity
{
    [Key] public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}