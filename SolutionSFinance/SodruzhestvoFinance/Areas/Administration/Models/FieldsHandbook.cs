using SFinance.Data.DataBase;

namespace SodruzhestvoFinance.Areas.Administration.Models
{
	public class FieldsHandbook
	{
		public int PageNum { get; set; }

		public int PageCount { get; set; }

		public string MessageText { get; set; }

		public HandbookEntity Handbook { get; set; }

		public List<FieldEntity> Fields { get; set; }

		public FieldEntity FieldUpdate { get; set; }
	}
}
