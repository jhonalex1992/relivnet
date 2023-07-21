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
    public class StateConfig : IEntityTypeConfiguration<StateEntity>
    {
        public void Configure(EntityTypeBuilder<StateEntity> builder)
        {
            builder.ToTable("states");
            builder.HasKey(e => e.StateId);
            builder.Property(u => u.StateId)
             .ValueGeneratedOnAdd();

            builder.HasMany(x => x.Products)
            .WithOne(y => y.State);
        }
    }
}
