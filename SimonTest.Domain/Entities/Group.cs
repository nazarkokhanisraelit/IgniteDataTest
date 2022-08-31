namespace SimonTest.Domain.Entities;

using Abstract;

#pragma warning disable CS8618
public class Group : Entity
{
    public string Name { get; set; }
    
    public List<UserGroups> UserGroups { get; set; } = new();
}