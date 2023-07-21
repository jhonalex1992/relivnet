###  Instalacion docker

docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-CU8-ubuntu

#### Adicion migrations

dotnet ef migrations add InitialCreate -c RelivnetDbContext -s ../relivnet.infraestructure.api --output-dir migrations
dotnet ef migrations script -o .\Migrations\Scripts\v1.0.0.sql -s ../relivnet.infraestructure.api
dotnet ef migrations add v1.0.0 -s ../relivnet.infraestructure.api
dotnet ef migrations remove -s ../relivnet.infraestructure.api
dotnet ef database update -c RelivnetDbContext -s ../relivnet.infraestructure.Api

#### Usuario administrador
user: admin@relivnet.com
password: admin2023


