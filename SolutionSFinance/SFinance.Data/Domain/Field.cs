using SFinance.Data.DataBase;

namespace SFinance.Data
{
    public class Field
    {
        public int IdField { get; set; }

        public string NameFieldToQuery { get; set; }

        public string NameVisible { get; set; }

        public int? IdRefHandbook { get; set; }

        public TypeData TypeData { get; set; }

        public bool IsVisible { get; set; }

        public bool IsEdit { get; set; }

        public bool IsNull { get; set; }

        public Field(FieldEntity entit)
        {
            IdField = entit.IdField;
            NameFieldToQuery = entit.NameToQuery;
            NameVisible = entit.NameVisible;
            IdRefHandbook = entit.RefHandbookToField;
            TypeData = (TypeData)entit.IdTypeData;
            IsVisible = entit.IsVisible;
            IsEdit = entit.IsEdit;
            IsNull = entit.IsNull;
        }
    }
}
