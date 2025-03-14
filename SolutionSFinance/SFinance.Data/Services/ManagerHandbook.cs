namespace SFinance.Data.Services
{
    public class ManagerHandbook : IManagerHandbook
    {
        private readonly IHandbookServices HandbookService;

        public ManagerHandbook(IHandbookServices handbookServices)
        {
            HandbookService = handbookServices;
        }

        public Handbook GenerateHandbook(int idHandbook)
        {
            var handbookEntity = HandbookService.GetHandbookById(idHandbook);

            HandbookBuilder handbookBuilder = new HandbookBuilder(handbookEntity.Id, handbookEntity.NameHandbook, handbookEntity.KeyField, handbookEntity.SelectionField);

            handbookBuilder.AddVisibleField(handbookEntity.Fields.ToList());

            List<Dictionary<string, object>> metaDataHandbook = HandbookService.GetDataFromDirectoryQuery(handbookEntity.Request);

            handbookBuilder.AddValuesFilds(metaDataHandbook);

            return handbookBuilder.Build();
        }

        public void InsertValue(int idHandbook, Dictionary<string, string> addedValues)
        {
            var handbook = HandbookService.GetHandbookById(idHandbook);

            HandbookService.InsertValueToTable(handbook.TableName, addedValues);
        }

        public void UpdateValue(int idHandbook, string valueKey,  Dictionary<string, string> addedValues)
        {
            var handbook = HandbookService.GetHandbookById(idHandbook);

            HandbookService.UpdateValueToTable(handbook.TableName, handbook.KeyField, valueKey, addedValues);
        }

        public void DeleteValue(int idHandbook, string valueKey)
        {
            var handbook = HandbookService.GetHandbookById(idHandbook);

            HandbookService.DeleteValueToTable(handbook.TableName, handbook.KeyField, valueKey);
        }
    }
}
