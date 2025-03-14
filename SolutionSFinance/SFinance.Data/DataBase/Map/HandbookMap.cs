using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFinance.Data.DataBase
{
    public class HandbookMap : IEntityTypeConfiguration<HandbookEntity>
    {
        public void Configure(EntityTypeBuilder<HandbookEntity> builder)
        {
            builder.ToTable("Handbook", "dbo");
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id)
                .HasColumnName("id")
                .HasColumnType("int");

            builder.Property(h => h.NameHandbook)
                .HasColumnName("nameHandbook")
                .HasColumnType("nvarchar")
                .HasMaxLength(100);

            builder.Property(h => h.TableName)
                .HasColumnName("tableName")
                .HasColumnType("nvarchar")
                .HasMaxLength(150);

            builder.Property(h => h.Request)
                .HasColumnName("request")
                .HasColumnType("nvarchar")
                .HasMaxLength(1000);

            builder.Property(h => h.KeyField)
                .HasColumnName("keyField")
                .HasColumnType("nvarchar")
                .HasMaxLength(100);

            builder.Property(h => h.SelectionField)
                .HasColumnName("selectionField")
                .HasColumnType("nvarchar")
                .HasMaxLength(100);

            builder.Property(h => h.Width)
                .HasColumnName("width")
                .HasColumnType("float");

            builder.Property(h => h.Height)
                .HasColumnName("height")
                .HasColumnType("float");

            builder.HasMany(h => h.Fields)
                .WithOne(f => f.Handbook)
                .HasForeignKey(f => f.IdHandbook);
        }
    }
}