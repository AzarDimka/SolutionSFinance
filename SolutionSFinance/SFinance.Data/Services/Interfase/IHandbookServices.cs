using SFinance.Data.DataBase;

namespace SFinance.Data.Services
{
    public interface IHandbookServices
    {
	    List<HandbookEntity> GetHandbookEntities();

		HandbookEntity GetHandbookById(int idHandbook);

		void UpdateHandbook(HandbookEntity model, out string message);

		void AddHandbook(HandbookEntity model, out string message);

		void DeleteHandbook(int idHandbook, out string message);

		List<FieldEntity> GetFieldsEntityByIdHandbook(int idHandbook);

		void UpdateField(FieldEntity model, out string message);

		void AddField(FieldEntity model, out string message);

		void DeleteField(int idField, out string message);

		List<Dictionary<string, object>> GetDataFromDirectoryQuery(string handbookQuery);

        void InsertValueToTable(string nameTable, Dictionary<string, string> addedValues);

        void UpdateValueToTable(string nameTable, string nameKeyField, string valueKey, Dictionary<string, string> addedValues);

        void DeleteValueToTable(string nameTable, string nameKeyField, string valueKey);

        Dictionary<string, string> GetKeyValueDictionary(int idHandbook, int idSelectedValue);

    }
}
