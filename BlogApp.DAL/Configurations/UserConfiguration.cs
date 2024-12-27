using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasIndex(x => x.UserName)
            .IsUnique();

        builder
            .HasIndex(x => x.Email)
            .IsUnique();

        builder
            .Property(x => x.Email)
            .HasMaxLength(128)
            .IsRequired();

        builder
            .Property(x => x.UserName)
            .HasMaxLength(32)
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasMaxLength(32)
            .IsRequired();

        builder
            .Property(x => x.Surname)
            .HasMaxLength(32)
            .IsRequired();
    }
}
