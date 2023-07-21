using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using relivnet.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relivnet.infraestructure.azsql.configs
{
    public class ProductConfig : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("products");

            builder.HasKey(e => e.ProductId); // Configuración de la clave primaria

            builder.Property(u => u.ProductId)
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.Category)
                .WithMany(y => y.Products)
                .HasForeignKey(z => z.CategoryId)
                .HasPrincipalKey(x => x.CategoryId);

            builder.HasOne(x => x.State)
                .WithMany(y => y.Products)
                .HasForeignKey(z => z.StateId)
                .HasPrincipalKey(x => x.StateId);

        }
    }
}
