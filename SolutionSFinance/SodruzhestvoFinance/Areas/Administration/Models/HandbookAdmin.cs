using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using SFinance.Data;
using SFinance.Data.DataBase;

namespace SodruzhestvoFinance.Areas.Administration.Models
{
	public class HandbookAdmin
	{
		public List<Handbook> Handbooks { get; set; }

		public HandbookEntity HandbookUpdate { get; set; }

		public string MessageText { get; set; }

		public int PageNum { get; set; }

		public int PageCount { get; set; }

		public string? InputField { get; set; }
	}
}