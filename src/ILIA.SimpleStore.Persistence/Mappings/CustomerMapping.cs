using ILIA.SimpleStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILIA.SimpleStore.Persistence.Mappings
{
    internal class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType("varchar(250)")
                .IsRequired();



            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.Id);

            builder.ToTable("Customers");
        }
    }
}
