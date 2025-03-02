using Microsoft.AspNetCore.Mvc;

namespace SodruzhestvoFinance.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class UserAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
