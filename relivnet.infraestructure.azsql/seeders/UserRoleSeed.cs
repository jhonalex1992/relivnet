using Microsoft.EntityFrameworkCore;
using relivnet.domain.entities;

namespace relivnet.infraestructure.azsql.seeders;

public static class UserRoleSeed
{
    public static void SeedUserRole(this ModelBuilder modelBuilder) {
        modelBuilder.Entity<UserRoleEntity>().HasData(
            new UserRoleEntity() { 
                Id = 1,
                UserId = 1,
                RoleId = 1,
                CreateDate = DateTime.Now,
                UserCreatedAt = "admin@relivnet.com"
            }
        );
    }
}