namespace BlogApp.Core.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public int Role { get; set; }
}
