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

        public float Height { get; set; }

        public  float Width { get; set; }

        public Handbook(int idHandbook, string nameHandbook, string keyField, string visibleField, float height, float width)
        {
            NameHandbook = nameHandbook;
            IdHandbook = idHandbook;
            Fields = new List<Field>();
            FieldsValue = new List<Dictionary<string, object>>();
            KeyField = keyField;
            VisibleField = visibleField;
            Height = height;
            Width = width;
        }
    }
}
