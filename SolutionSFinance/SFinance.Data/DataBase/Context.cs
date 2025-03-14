using Microsoft.EntityFrameworkCore;

namespace SFinance.Data.DataBase
{
    /// <summary>
    /// �������� ���� ������
    /// </summary>
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// ���� ������������
        /// </summary>
        public DbSet<FieldEntity> Fields { get; set; }

        /// <summary>
        /// �����������
        /// </summary>
        public DbSet<HandbookEntity> Handbooks { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public DbSet<ProductEntity> Products { get; set; }

        /// <summary>
        /// ���� ������ ��� ����
        /// </summary>
        public DbSet<TypeDataEntity> TypeDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FieldMap());
            modelBuilder.ApplyConfiguration(new HandbookMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new TypeDataMap());
        }
    }
}