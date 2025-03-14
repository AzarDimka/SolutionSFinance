namespace SFinance.Data
{
    public class Field
    {
        public int IdField { get; set; }

        public string NameFieldToQuery { get; set; }

        public string NameVisible { get; set; }

        public int? IdRefHandbook { get; set; }

        public TypeData TypeData { get; set; }

        public Field(int idField, string nameFieldToQuery, string nameVisible, TypeData typeData, int? idRefHandbook = null)
        {
            IdField = idField;
            NameFieldToQuery = nameFieldToQuery;
            NameVisible = nameVisible;
            IdRefHandbook = idRefHandbook;
            TypeData = typeData;
        }
    }
}
