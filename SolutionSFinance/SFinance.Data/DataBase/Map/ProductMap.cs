using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFinance.Data.DataBase
{
    public class ProductMap : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Product", "dbo");
            builder.HasKey(t => t.IdProduct);

            builder.Property(t => t.NameProduct)
                .HasColumnName("nameProduct")
                .HasColumnType("nvarchar")
                .HasMaxLength(150);
        }
    }
}