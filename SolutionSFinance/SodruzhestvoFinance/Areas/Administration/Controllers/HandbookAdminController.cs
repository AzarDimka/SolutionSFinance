using Microsoft.AspNetCore.Mvc;
using SFinance.Data;
using SFinance.Data.DataBase;
using SFinance.Data.Services;
using SodruzhestvoFinance.Areas.Administration.Models;

namespace SodruzhestvoFinance.Areas.Administration.Controllers
{
	[Area("Administration")]
	public class HandbookAdminController : Controller
	{
		private const int SizePage = 10;

		public IHandbookServices HandbookServices { get; set; }

		public HandbookAdminController(IHandbookServices handbookServices)
		{
			HandbookServices = handbookServices;
		}

		public IActionResult Index(int pageNum = 0, string inputField = null)
		{
			var handbookEntity = HandbookServices.GetHandbookEntities();

			if (inputField != null)
			{
				handbookEntity = handbookEntity.Where(w => w.NameHandbook.ToLower().Contains(inputField.ToLower())).ToList();
			}

			var handbooks = GetHandbooksToEntities(handbookEntity).Skip(SizePage * pageNum).Take(SizePage).ToList();

			int pageCount = Convert.ToInt32(Math.Ceiling((decimal)handbooks.Count / SizePage));

			var model = new HandbookAdmin()
			{
				Handbooks = handbooks,
				InputField = inputField,
				PageNum = pageNum,
				PageCount = pageCount
			};

			return View("Index", model);
        }

        public IActionResult UpdateHandbook(int idHandbook)
        {
	        var handbookUpdate = HandbookServices.GetHandbookById(idHandbook);

	        var model = new HandbookAdmin()
	        {
				HandbookUpdate = handbookUpdate
			};

			return View("UpdateHandbook", model);
		}

		public IActionResult Update(HandbookAdmin handbookModel)
		{
			HandbookServices.UpdateHandbook(handbookModel.HandbookUpdate, out string messageText);

			var model = new HandbookAdmin()
			{
				HandbookUpdate = handbookModel.HandbookUpdate,
				MessageText = messageText
			};

			return View("UpdateHandbook", model);
		}

        public IActionResult AddHandbook()
        {
	        return View("AddHandbook", null);
		}

		public IActionResult Add(HandbookAdmin handbookModel)
		{
			HandbookServices.AddHandbook(handbookModel.HandbookUpdate, out string messageText);

			var model = new HandbookAdmin()
			{
				MessageText = messageText
			};
			return View("AddHandbook", model);
		}

		public IActionResult DeleteHandbook(int idHandbook, int pageNum, string inputField)
        {
			HandbookServices.DeleteHandbook(idHandbook, out string messageText);

			var handbookEntity = HandbookServices.GetHandbookEntities();

			if (inputField != null)
			{
				handbookEntity = handbookEntity.Where(w => w.NameHandbook.ToLower().Contains(inputField.ToLower())).ToList();
			}

			var handbooks = GetHandbooksToEntities(handbookEntity).Skip(SizePage * pageNum).Take(SizePage).ToList();

			int pageCount = Convert.ToInt32(Math.Ceiling((decimal)handbooks.Count / SizePage));

			var model = new HandbookAdmin()
			{
				Handbooks = handbooks,
				InputField = inputField,
				PageCount = pageCount,
				PageNum = pageNum,
				MessageText = messageText
			};

			return View("Index", model);
		}

		private List<Handbook> GetHandbooksToEntities(List<HandbookEntity> entities)
        {
	        List<Handbook> resultList = new List<Handbook>();

	        foreach (var entity in entities)
	        {
		        var handbook = new Handbook(
			        entity.Id,
			        entity.NameHandbook,
			        entity.KeyField,
			        entity.SelectionField,
			        entity.Height,
			        entity.Width
			     );

				resultList.Add(handbook);
			}

	        return resultList;
        }
	}
}
