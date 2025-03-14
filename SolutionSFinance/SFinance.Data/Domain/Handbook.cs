namespace SFinance.Data
{
    public class Handbook
    {
        public int IdHandbook { get; set; }

        public string NameHandbook { get; set; }

        public string KeyField { get; set; }

        public string VisibleField { get; set; }

        public List<Field> Fields { get; set; }

        public List<Dictionary<string, object>> FieldsValue { get; set; }

        public Handbook(int idHandbook, string nameHandbook, string keyField, string visibleField)
        {
            NameHandbook = nameHandbook;
            IdHandbook = idHandbook;
            Fields = new List<Field>();
            FieldsValue = new List<Dictionary<string, object>>();
            KeyField = keyField;
            VisibleField = visibleField;
        }
    }
}
