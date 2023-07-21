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
    public class CategoryConfig : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("categories");
            builder.HasKey(e => e.CategoryId);
            builder.Property(u => u.CategoryId)
             .ValueGeneratedOnAdd();

            builder.HasMany(x => x.Products)
            .WithOne(y => y.Category);
        }
    }
}
