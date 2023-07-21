namespace relivnet.domain.entities;

public class UserEntity : BaseEntity
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string FirstName { get; set; }
    public string? SecondName { get; set; }
    public string FirstSurname { get; set; }
    public string? SecondSurname { get; set; }
    public int Gender { get; set; }
    public DateTime Birthdate { get; set; } 
    public string? Cellphone { get; set; }
    public string? Password { get; set; }
    public string Country { get; set; }
    public string CountryCode { get; set; }
    public string Region { get; set; }
    public string RegionName { get; set; }
    public string City { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? Timezone { get; set; }
    
    public List<UserRoleEntity> UsersRoles { get; set; }
}