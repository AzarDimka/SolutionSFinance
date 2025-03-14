namespace SFinance.Data.Services
{
    public interface IManagerHandbook
    {
        Handbook GenerateHandbook(int idHandbook);

        void InsertValue(int idHandbook, Dictionary<string, string> addedValues);

        void UpdateValue(int idHandbook, string valueKey, Dictionary<string, string> addedValues);

        void DeleteValue(int idHandbook, string valueKey);
    }
}
