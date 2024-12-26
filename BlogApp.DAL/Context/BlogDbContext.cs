using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DAL.Context;

public class BlogDbContext : DbContext
{

    public BlogDbContext(DbContextOptions opt) : base(opt)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Category> Categories { get; set; }
}
