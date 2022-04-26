namespace teleperformance_case3.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Mail { get; set; }
    public string HashedPassword { get; set; }
    public string Salt { get; set; }
    public string Role { get; set; } = "User";
    public List<ShoppingList> ShoppingLists { get; set; }
}