using System.Collections.Generic;

namespace SFinance.Data.DataBase
{
    /// <summary>
    /// Entity модель справочника
    /// </summary>
    public class HandbookEntity
    {
        /// <summary>
        /// Идентификатор справочника
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название справочника
        /// </summary>
        public string NameHandbook { get; set; }

        /// <summary>
        /// Название таблицы справочника
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Запрос справочника
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// Ключевое поле
        /// </summary>
        public string KeyField { get; set; }

        /// <summary>
        /// Поле выбора
        /// </summary>
        public string SelectionField { get; set; }

        /// <summary>
        /// Ширина справочника
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Высота справочника
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Ссылки на регистрации
        /// </summary>
        public ICollection<FieldEntity> Fields { get; set; }
    }
}