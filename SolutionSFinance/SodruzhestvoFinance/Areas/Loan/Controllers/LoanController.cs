using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SodruzhestvoFinance.Data;
using SodruzhestvoFinance.Areas.Loan.Models;
using SodruzhestvoFinance.Areas.Employees.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using SodruzhestvoFinance.Areas.Loan.Enum;


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
        public IActionResult LoanCreationPage()
        {
            return View("Create");
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
                    Status = LoanStatusType.newLoan.ToString() // Или используйте enum: LoanStatus.Active.ToString()
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

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            return View(loan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSave(int id, LoanIssueViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var loan = await _context.Loans.FindAsync(id);
                    if (loan == null)
                    {
                        return NotFound();
                    }

                    loan.EmployeeId = viewModel.EmployeeId;
                    loan.LoanAmount = viewModel.LoanAmount;
                    loan.InterestRate = viewModel.InterestRate;
                    loan.LoanTerm = viewModel.LoanTerm;
                    loan.IssueDate = viewModel.IssueDate;

                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(viewModel.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            viewModel.Employees = _context.Employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = e.LastName
            }).ToList();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.LoanId == id);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var loans = await _context.Loans.FindAsync(id);

            if (loans == null)
            {
                //return Json(new { success = false, message = "Сотрудник не найден" }); // Возвращаем JSON с ошибкой
                return NotFound();
            }

            _context.Loans.Remove(loans);
            await _context.SaveChangesAsync();

            //return Json(new { success = true, message = "Сотрудник успешно удален" }); // Возвращаем JSON об успехе
            return RedirectToAction(nameof(Index));
        }

    }
}
