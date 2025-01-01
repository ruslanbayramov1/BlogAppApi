using BlogApp.Core.Entities;

namespace BlogApp.BL.ExternalServices.Interfaces;

public interface IJwtService
{
    string CreateToken(User user, int hours = 36);
}
