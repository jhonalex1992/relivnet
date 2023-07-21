using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using relivnet.domain.entities;

namespace relivnet.infraestructure.azsql.configs;

public class UserRoleConfig: IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.ToTable("users_roles");
        builder.HasKey(x => x.Id);
        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();
        
        builder.HasOne(x => x.User)
            .WithMany(y => y.UsersRoles)
            .HasForeignKey(z => z.UserId)
            .HasPrincipalKey(x => x.Id);
        
        builder.HasOne(x => x.Role)
            .WithMany(y => y.UsersRoles)
            .HasForeignKey(z => z.RoleId)
            .HasPrincipalKey(x => x.Id);
    }
}