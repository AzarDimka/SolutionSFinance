namespace SFinance.Data.DataBase
{
    /// <summary>
    /// Поле справочника
    /// </summary>
    public class FieldEntity
    {
        /// <summary>
        /// Идентификатор поля справочника
        /// </summary>
        public int IdField { get; set; }

        /// <summary>
        /// Идентификатор справочника
        /// </summary>
        public int IdHandbook { get; set; }

        /// <summary>
        /// Ссылка на объект справочника
        /// </summary>
        public HandbookEntity Handbook { get; set; }

        /// <summary>
        /// Индекс поля в справочнике
        /// </summary>
        public int IndexField { get; set; }

        /// <summary>
        /// Название поля в запросе
        /// </summary>
        public string NameToQuery { get; set; }

        /// <summary>
        /// Название, которое будет отображено
        /// </summary>
        public string NameVisible { get; set; }

        /// <summary>
        /// Тип данных поля
        /// </summary>
        public int IdTypeData { get; set; }

        /// <summary>
        /// Ссылка на объект тип данных
        /// </summary>
        public TypeDataEntity TypeDataEntity { get; set; }
    
        /// <summary>
        /// Ссылка на справочник, по полю (если тип данных "Справочник", то это свойтво id справочника)
        /// </summary>
        public int? RefHandbookToField  { get; set; }

        /// <summary>
        /// Необходимо отображать
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Можно редактировать
        /// </summary>
        public bool IsEdit { get; set; }

        /// <summary>
        /// Поле может содержать NULL
        /// </summary>
        public bool IsNull { get; set; }
        
        /// <summary>
        /// Поле проверяется на дубли
        /// </summary>
        public bool IsCheckDuplicate { get; set; }
    }
}