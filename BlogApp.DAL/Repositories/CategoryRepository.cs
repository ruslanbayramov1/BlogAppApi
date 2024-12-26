using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Context;

namespace BlogApp.DAL.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(BlogDbContext context) : base(context)
    {
        
    }
}
