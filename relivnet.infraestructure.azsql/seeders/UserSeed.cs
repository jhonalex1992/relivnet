using relivnet.domain.entities;
using Microsoft.EntityFrameworkCore;

namespace relivnet.infraestructure.azsql.seeders
{
    public static class UserSeed
    {
        public static void SeedUser(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity() { 
                    Id = 1,
                    FirstName = "Jhon",
                    SecondName= "Alexander",
                    FirstSurname = "Salazar",
                    SecondSurname = "Achicanoy",
                    Cellphone = "+573017178883",
                    Email= "admin@relivnet.com",
                    Password = "AQAAAAIAAYagAAAAEBYuwTqBi2df+hTorDAafa6lvZlMiyfWOVs9it5l7LqXxSV3cMMQcIwqVOn/dVSAPA==",
                    Country = "Ecuador",
                    CountryCode = "EC",
                    Region = "P",
                    RegionName = "Provincia de Pichincha",
                    City = "Quito",
                    Latitude = -0.2309,
                    Longitude = -78.5211,
                    Timezone  = "America/Guayaquil",
                    CreateDate = DateTime.Now,
                    Birthdate = DateTime.Now,
                    Gender = 1,
                    UserCreatedAt = "admin@relivnet.com"
                }
            );
        }
    }
}
