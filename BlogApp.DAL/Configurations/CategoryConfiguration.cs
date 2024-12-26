using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DAL.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .HasIndex(x => x.Name)
            .IsUnique();

        builder
            .Property(x => x.Name)
            .HasMaxLength(32);

        builder
            .Property(x => x.Icon)
            .HasMaxLength(128);
    }
}
