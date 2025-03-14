using SFinance.Data.DataBase;

namespace SFinance.Data.Services
{
    public class HandbookBuilder
    {
        private Handbook Handbook;

        public HandbookBuilder(int idHandbook, string nameHandbook, string fieldKey, string visibleField)
        {
            Handbook = new Handbook(idHandbook, nameHandbook, fieldKey, visibleField);
        }

        public void AddVisibleField(List<FieldEntity> fieldEntities)
        {
            foreach (var entity in fieldEntities)
            {
                if (entity.IsVisible)
                {
                    var field = new Field(entity.IdField, entity.NameToQuery, entity.NameVisible, (TypeData)entity.IdTypeData);

                    Handbook.Fields.Add(field);
                }
            }
        }

        public void AddValuesFilds(List<Dictionary<string, object>> values)
        {
            if (!values.Any())
            {
                return;
            }

            var visibleFields = GetVisibleFields();

            foreach (Dictionary<string, object> value in values)
            {
                Dictionary<string, object> resutl = new Dictionary<string, object>();

                foreach(var objValue in value)
                {
                    if (visibleFields.Contains(objValue.Key))
                    {
                        resutl.Add(objValue.Key, objValue.Value);
                    }
                }

                if (resutl.Count > 0)
                {
                    Handbook.FieldsValue.Add(resutl);
                }

            }

        }

        private List<string> GetVisibleFields()
        {
            List<string> fields = new List<string>();

            foreach (var field in Handbook.Fields)
            {
                fields.Add(field.NameFieldToQuery);
            }

            return fields;
        }

        public Handbook Build()
        {
            return Handbook;
        }
    }
}
