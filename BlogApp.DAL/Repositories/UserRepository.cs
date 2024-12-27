using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DAL.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(BlogDbContext context) : base(context)
    {
    }

    public Task<User?> GetByUsernameAsync(string username) 
        => Table.FirstOrDefaultAsync(x => x.UserName == username);
}
