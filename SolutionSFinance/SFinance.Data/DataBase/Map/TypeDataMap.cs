using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFinance.Data.DataBase
{
    public class TypeDataMap : IEntityTypeConfiguration<TypeDataEntity>
    {
        public void Configure(EntityTypeBuilder<TypeDataEntity> builder)
        {
            builder.ToTable("TypeData", "dbo");
            builder.HasKey(t => t.IdType);

            builder.Property(t => t.IdType)
                .HasColumnName("idType")
                .HasColumnType("int");

            builder.Property(t => t.FullName)
                .HasColumnName("fullName")
                .HasColumnType("nvarchar")
                .HasMaxLength(150);

            builder.HasMany(t => t.Fields)
                .WithOne(f => f.TypeDataEntity)
                .HasForeignKey(f => f.IdTypeData);
        }
    }
}