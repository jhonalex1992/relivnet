namespace relivnet.domain.entities;

public class UserRoleEntity : BaseEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public UserEntity User { get; set; }
    public RoleEntity Role { get; set; }
}