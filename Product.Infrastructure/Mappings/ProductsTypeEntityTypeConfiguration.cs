using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Model.Models;
using System.Data;

namespace Product.Infrastructure.Mappings
{
    public class ProductsTypeEntityTypeConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(512)
                .HasColumnType<string>(nameof(SqlDbType.NVarChar));

            builder.Property(x => x.ProductDate)
                .IsRequired()
                .HasColumnType<DateTime>(nameof(SqlDbType.DateTime));

            builder.Property(x => x.ManufacturePhone)
               .IsRequired()
               .HasMaxLength(11)
               .HasColumnType<string>(nameof(SqlDbType.NVarChar));

            builder.Property(x => x.ManufacturePhone)
               .IsRequired()
               .HasMaxLength(11)
               .HasColumnType<string>(nameof(SqlDbType.NVarChar));

            builder.Property(x => x.ManufactureEmail)
               .IsRequired()
               .HasMaxLength(512)
               .HasColumnType<string>(nameof(SqlDbType.NVarChar));


            builder.Property(x => x.IsAvailable)
              .IsRequired()
              .HasColumnType<bool>(nameof(SqlDbType.Bit));
        }
        }
}
