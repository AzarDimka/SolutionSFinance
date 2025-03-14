using SFinance.Data.DataBase;

namespace SFinance.Data.Services
{
    public interface IHandbookServices
    {
        HandbookEntity GetHandbookById(int idHandbook);

        List<Dictionary<string, object>> GetDataFromDirectoryQuery(string handbookQuery);

        void InsertValueToTable(string nameTable, Dictionary<string, string> addedValues);

        void UpdateValueToTable(string nameTable, string nameKeyField, string valueKey, Dictionary<string, string> addedValues);

        void DeleteValueToTable(string nameTable, string nameKeyField, string valueKey);
    }
}
