namespace SimonTest.Infrastructure.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

public class UserGroupsConfiguration : IEntityTypeConfiguration<UserGroups>
{
    public void Configure(EntityTypeBuilder<UserGroups> builder)
    {
        builder.ToTable("UserGroups");
    }
}