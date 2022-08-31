#pragma warning disable CS8618
namespace SimonTest.Domain.Entities;

using Abstract;

public class User : Entity
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    
    public string Email { get; set; }

    public List<UserGroups> UserGroups { get; set; } = new();
}