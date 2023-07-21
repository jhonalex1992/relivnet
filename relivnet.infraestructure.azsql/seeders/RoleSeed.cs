using Microsoft.EntityFrameworkCore;
using relivnet.domain.entities;

namespace relivnet.infraestructure.azsql.seeders;

public static class RoleSeed
{
    public static void SeedRole(this ModelBuilder modelBuilder) {
        modelBuilder.Entity<RoleEntity>().HasData(
            new RoleEntity() { 
                Id = 1,
                Key = "ADMIN",
                Name = "Administrador",
                CreateDate = DateTime.Now,
                UserCreatedAt = "admin@relivnet.com"
            },
            new RoleEntity() { 
                Id = 2,
                Key = "PUBLISHER",
                Name = "Publicador",
                CreateDate = DateTime.Now,
                UserCreatedAt = "publisher@relivnet.com"
            }
        );
    }
}