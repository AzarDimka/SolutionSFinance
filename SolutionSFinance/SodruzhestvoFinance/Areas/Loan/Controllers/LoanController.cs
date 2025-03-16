using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SodruzhestvoFinance.Data;
using SodruzhestvoFinance.Areas.Loan.Models;
using SodruzhestvoFinance.Areas.Employees.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace SodruzhestvoFinance.Areas.Loan.Controllers
{
    [Area("Loan")]
    public class LoanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoanController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var loans = _context.Loans.Include(l => l.Employee).ToList();
            return View(loans);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // 1. Получение списка сотрудников из базы данных
            var employees = _context.Employees.ToList();

            // 2. Создание списка SelectListItem для DropDownList
            var employeeList = employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = e.LastName // Или e.FirstName + " " + e.LastName, в зависимости от того, как вы храните имя
            }).ToList();

            // 3. Создание ViewModel и передача списка сотрудников
            var viewModel = new LoanIssueViewModel
            {
                Employees = employeeList,
                IssueDate = DateTime.Today //Установим значение по умолчанию
            };

            // 4. Передача ViewModel в представление
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LoanIssueViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Создание объекта Loan на основе данных из ViewModel
                var loan = new Models.Loan
                {
                    EmployeeId = model.EmployeeId,
                    LoanAmount = model.LoanAmount,
                    InterestRate = model.InterestRate,
                    LoanTerm = model.LoanTerm,
                    IssueDate = model.IssueDate,
                    CurrentBalance = model.LoanAmount, // Изначально остаток равен сумме займа
                    Status = "Активный" // Или используйте enum: LoanStatus.Active.ToString()
                };

                // 2. Добавление займа в базу данных
                _context.Loans.Add(loan);
                _context.SaveChanges();

                // 3. Перенаправление на страницу просмотра информации о займе (пока не реализована) или на список займов
                return RedirectToAction("Index"); // Перенаправление на Index для просмотра созданного займа
            }

            // Если модель не валидна, возвращаем представление с ViewModel для отображения ошибок
            // Необходимо повторно загрузить список сотрудников для DropDownList
            var employees = _context.Employees.ToList();
            model.Employees = employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = e.LastName
            }).ToList();

            return View(model);
        }
    }
}
