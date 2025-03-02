using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SodruzhestvoFinance.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class UserAccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserAccountController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Get the list of users
            var users = await _userManager.Users.ToListAsync(); // Use ToListAsync for better performance
            //  var users =  _userManager.Users.ToList();

            // Pass the list of users to the view using a ViewModel (recommended)
            // In this example, we're just using ViewBag for simplicity
            ViewBag.Users = users;

            return View();
        }
    }
}
