namespace relivnet.domain.entities;

public abstract class BaseEntity
{
    
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string UserCreatedAt { get; set; }
    public string? UserUpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDelete { get; set; } = false;
    
}