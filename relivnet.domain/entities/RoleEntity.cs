namespace relivnet.domain.entities;

public class RoleEntity : BaseEntity
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Name { get; set; }
    
    public List<UserRoleEntity> UsersRoles { get; set; }
}