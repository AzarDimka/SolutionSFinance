using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFinance.Data.DataBase
{
    public class FieldMap : IEntityTypeConfiguration<FieldEntity>
    {
        public void Configure(EntityTypeBuilder<FieldEntity> builder)
        {
            builder.ToTable("Field", "dbo");
            builder.HasKey(f => f.IdField);

            builder.Property(f => f.IdField)
                .HasColumnName("idField")
                .HasColumnType("int");

            builder.Property(f => f.IdHandbook)
                .HasColumnName("idHandbook")
                .HasColumnType("int");

            builder.Property(f => f.IndexField)
                .HasColumnName("indexField")
                .HasColumnType("int");

            builder.Property(f => f.NameToQuery)
                .HasColumnName("nameToQuery")
                .HasColumnType("nvarchar")
                .HasMaxLength(100);

            builder.Property(f => f.NameVisible)
                .HasColumnName("nameVisible")
                .HasColumnType("nvarchar")
                .HasMaxLength(100);

            builder.Property(f => f.IdTypeData)
                .HasColumnName("idTypeData")
                .HasColumnType("int");

            builder.Property(f => f.RefHandbookToField)
                .HasColumnName("refHandbookToField")
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property<bool>(f => f.IsVisible)
                .HasColumnName("isVisible")
                .HasColumnType("bit");

            builder.Property<bool>(f => f.IsEdit)
                .HasColumnName("isEdit")
                .HasColumnType("bit");

            builder.Property<bool>(f => f.IsNull)
                .HasColumnName("isNull")
                .HasColumnType("bit");

            builder.Property<bool>(f => f.IsCheckDuplicate)
                .HasColumnName("isCheckDuplicate")
                .HasColumnType("bit");
        }
    }
}