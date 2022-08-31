namespace SimonTest.Infrastructure.Persistence;

using Domain.Entities;
using EntityConfigurations;
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    
    public DbSet<UserGroups> UserGroups { get; set; }
    
    public DbSet<Group> Groups { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}