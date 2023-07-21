
using relivnet.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace relivnet.infraestructure.azsql.configs
{
    public class UserConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);
            builder.Property(u => u.Id)
                   .ValueGeneratedOnAdd();
            builder.Property(u => u.Email)
                .HasMaxLength(128);
            builder.HasIndex(u => u.Email)
                .IsUnique();
            builder.Property(u => u.FirstName)
                .HasMaxLength(32);
            builder.Property(u => u.SecondName)
                .HasMaxLength(32);
            builder.Property(u => u.FirstSurname)
                .HasMaxLength(32);
            builder.Property(u => u.SecondSurname)
                .HasMaxLength(32);
            builder.Property(u => u.Cellphone)
                .HasMaxLength(32);
            builder.Property(u => u.Password)
                .HasMaxLength(256);
            builder.Property(u => u.Country)
                .HasMaxLength(16);
            builder.Property(u => u.CountryCode)
                .HasMaxLength(8);
            builder.Property(u => u.Region)
                .HasMaxLength(8);
            builder.Property(u => u.RegionName)
                .HasMaxLength(256);
            builder.Property(u => u.City)
                .HasMaxLength(32);
            builder.Property(u => u.Timezone)
                .HasMaxLength(64);
            
            builder.HasMany(x => x.UsersRoles)
                .WithOne(y => y.User);
        }
    }
}
