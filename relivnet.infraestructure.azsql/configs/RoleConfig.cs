using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using relivnet.domain.entities;

namespace relivnet.infraestructure.azsql.configs;

public class RoleConfig : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(x => x.Id);
        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();
        builder.HasIndex(u => u.Key)
            .IsUnique();
        builder.Property(u => u.Key)
            .HasMaxLength(16);
        builder.Property(u => u.Name)
            .HasMaxLength(16);
        
        builder.HasMany(x => x.UsersRoles).WithOne(y => y.Role);
    }
}