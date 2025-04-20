namespace QuizItEasy.Domain.Entities.Common;

public abstract class AuditableEntity
{
    public bool IsDeleted { get; protected set; } = false;
    
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public string CreatedBy { get; protected set; } = "admin";
    
    public DateTime LastModifiedAt { get; protected set; }
    public string LastModifiedBy { get; protected set; }
}
