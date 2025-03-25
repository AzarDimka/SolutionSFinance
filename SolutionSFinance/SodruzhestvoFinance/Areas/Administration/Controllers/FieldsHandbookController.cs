using Microsoft.AspNetCore.Mvc;
using SFinance.Data.Services;
using SodruzhestvoFinance.Areas.Administration.Models;

namespace SodruzhestvoFinance.Areas.Administration.Controllers
{
	[Area("Administration")]
	public class FieldsHandbookController : Controller
	{
		private const int SizePage = 10;

		public IHandbookServices HandbookServices { get; set; }

		public FieldsHandbookController(IHandbookServices handbookServices)
	    {
		    HandbookServices = handbookServices;
		}

		public IActionResult Index(int idHandbook, int pageNum = 0)
		{
			var handbook = HandbookServices.GetHandbookById(idHandbook);

			var fields = HandbookServices.GetFieldsEntityByIdHandbook(idHandbook).Skip(SizePage * pageNum).Take(SizePage).ToList();

			int pageCount = Convert.ToInt32(Math.Ceiling((decimal)fields.Count / SizePage));

	        FieldsHandbook modelFieldsHandbook = new FieldsHandbook()
	        {
				Handbook = handbook,
				PageNum = pageNum,
				PageCount = pageCount,
				Fields = fields
			};

            return View("Index", modelFieldsHandbook);
        }

		public IActionResult AddField(int idHandbook)
		{
			var handbook = HandbookServices.GetHandbookById(idHandbook);

			FieldsHandbook modelFieldsHandbook = new FieldsHandbook()
			{
				Handbook = handbook
			};

			return View("AddField", modelFieldsHandbook);
		}

		public IActionResult Add(FieldsHandbook model)
		{
			var handbook = HandbookServices.GetHandbookById(model.FieldUpdate.IdHandbook);

			HandbookServices.AddField(model.FieldUpdate, out string messageText);
			
			FieldsHandbook modelFieldsHandbook = new FieldsHandbook()
			{
				Handbook = handbook,
				MessageText = messageText
			};

			return View("AddField", modelFieldsHandbook);
		}

		public IActionResult UpdateField(int idHandbook, int idField)
		{
			var handbook = HandbookServices.GetHandbookById(idHandbook);

			var field = handbook.Fields.Where(w => w.IdField == idField).FirstOrDefault();

			FieldsHandbook modelFieldsHandbook = new FieldsHandbook()
			{
				Handbook = handbook,
				FieldUpdate = field
			};

			return View("UpdateField", modelFieldsHandbook);
		}

		public IActionResult Update(FieldsHandbook model)
		{
			var handbook = HandbookServices.GetHandbookById(model.FieldUpdate.IdHandbook);

			HandbookServices.UpdateField(model.FieldUpdate, out string messageText);
			
			FieldsHandbook modelFieldsHandbook = new FieldsHandbook()
			{
				Handbook = handbook,
				FieldUpdate = model.FieldUpdate,
				MessageText = messageText
			};

			return View("UpdateField", modelFieldsHandbook);
		}

		public IActionResult Delete(int idField, int idHandbook, int pageNum)
		{
			HandbookServices.DeleteField(idField, out string messageText);

			var handbook = HandbookServices.GetHandbookById(idHandbook);

			var fields = HandbookServices.GetFieldsEntityByIdHandbook(idHandbook).Skip(SizePage * pageNum).Take(SizePage).ToList();

			int pageCount = Convert.ToInt32(Math.Ceiling((decimal)fields.Count / SizePage));

			FieldsHandbook modelFieldsHandbook = new FieldsHandbook()
			{
				Handbook = handbook,
				PageNum = pageNum,
				PageCount = pageCount,
				Fields = fields,
				MessageText = messageText
			};

			return View("Index", modelFieldsHandbook);
		}
	}
}
