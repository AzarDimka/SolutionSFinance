namespace SFinance.Data.DataBase
{
    /// <summary>
    /// Entity модель типов данных для поля
    /// </summary>
    public class TypeDataEntity
    {
        /// <summary>
        /// Идентификатор типа даннных
        /// </summary>
        public int IdType { get; set; }

        /// <summary>
        /// Полное название типа данных
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Ссылка на объект поле справочника
        /// </summary>
        public ICollection<FieldEntity> Fields { get; set; }
    }
}