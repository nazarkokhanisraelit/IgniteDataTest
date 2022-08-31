namespace SimonTest.Domain.Entities;

using Abstract;

#pragma warning disable CS8618
public class UserGroups : Entity
{
    public Guid UserId { get; set; }

    public User User { get; set; }
    
    public Guid GroupId { get; set; }

    public Group Group { get; set; }
}