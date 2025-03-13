using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SodruzhestvoFinance.Areas.Administration.Models;

namespace SodruzhestvoFinance.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class RoleController : Controller
    {
        RoleManager<ApplicationRole> _manager;
        UserManager<ApplicationUser> _users;

        public RoleController(RoleManager<ApplicationRole> manager, UserManager<ApplicationUser> users)
        {
            _manager = manager;
            _users = users;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_manager.Roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            await _manager.CreateAsync(new ApplicationRole()
            {
                Name = roleName
            });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _manager.FindByIdAsync(id);
            if (role != null)
                await _manager.DeleteAsync(role);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Отображает страницу редактирования роли, включая информацию о роли и списках пользователей, являющихся и не являющихся ее членами.
        /// </summary>
        /// <param name="id">ID роли, которую необходимо отредактировать.</param>
        /// <returns>Представление (View) с моделью RoleEdit, содержащей информацию о роли и списках пользователей.</returns>
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            ApplicationRole role = await _manager.FindByIdAsync(id);
            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();
            foreach (ApplicationUser user in _users.Users)
            {
                var list = await _users.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        /// <summary>
        /// Обрабатывает отправку формы редактирования роли, обновляя имя роли и членство пользователей.
        /// </summary>
        /// <param name="model">Модель RoleModification, содержащая данные формы, включая ID роли, новое имя роли и списки пользователей для добавления/удаления.</param>
        /// <returns>Перенаправление на страницу списка ролей (Index) после успешного обновления.  Возвращает BadRequest в случае ошибок добавления/удаления пользователей.</returns>
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] RoleModification model)
        {
            var role = await _manager.FindByIdAsync(model.RoleId);
            role.Name = model.RoleName;
            await _manager.UpdateAsync(role);
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    ApplicationUser user = await _users.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _users.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            return BadRequest(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    ApplicationUser user = await _users.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _users.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            return BadRequest(result);
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
