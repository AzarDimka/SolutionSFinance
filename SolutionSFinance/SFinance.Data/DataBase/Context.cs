using Microsoft.EntityFrameworkCore;

namespace SFinance.Data.DataBase
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Поля справочников
        /// </summary>
        public DbSet<FieldEntity> Fields { get; set; }

        /// <summary>
        /// Справочники
        /// </summary>
        public DbSet<HandbookEntity> Handbooks { get; set; }

        /// <summary>
        /// Продукты
        /// </summary>
        public DbSet<ProductEntity> Products { get; set; }

        /// <summary>
        /// Типы данных для поля
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