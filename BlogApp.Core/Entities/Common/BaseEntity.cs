namespace BlogApp.Core.Entities.Common;

public class BaseEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdateAt { get; set; }
}
