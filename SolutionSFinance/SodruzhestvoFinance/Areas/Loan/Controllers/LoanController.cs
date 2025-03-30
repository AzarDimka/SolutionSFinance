using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SodruzhestvoFinance.Areas.Loan.Models;
using SodruzhestvoFinance.Data;


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
            var loans = _context.Loan.Include(l => l.Employee)
                .Include(l => l.LoanStatus);

            // Создаем список типов операций для выпадающего списка
            var operationTypes = new List<SelectListItem>
                 {
                     new SelectListItem { Value = "IssueLoan", Text = "Выдача займа" },
                     new SelectListItem { Value = "Payment", Text = "Внесение платежа" },
                     // ... другие типы операций ...
                 };

            ViewBag.OperationTypes = operationTypes;

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
            // 1. Получаем статус "Новый" из базы данных (предполагая, что он уже есть в таблице LoanStatus)
            var newLoanStatus = _context.LoanStatus.FirstOrDefault(ls => ls.LoanStatusId == 1);

            // 2. Создание объекта Loan на основе данных из ViewModel
            var loan = new Models.Loan
            {
                EmployeeId = model.EmployeeId,
                LoanAmount = model.LoanAmount,
                InterestRate = model.InterestRate,
                LoanTerm = model.LoanTerm,
                IssueDate = model.IssueDate,
                CurrentBalance = model.LoanAmount * model.InterestRate, // Изначально остаток равен сумме займа
                LoanStatusId = newLoanStatus.LoanStatusId // Устанавливаем статус "Новый"
            };

            // 3. Добавление займа в базу данных
            _context.Loan.Add(loan);
            _context.SaveChanges();

            // 4. Перенаправление на страницу просмотра информации о займе или на список займов
            return RedirectToAction("Index"); // Перенаправление на Index для просмотра созданного займа
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan.FindAsync(id);
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
                    var loan = await _context.Loan.FindAsync(id);
                    if (loan == null)
                    {
                        return NotFound();
                    }

                    loan.EmployeeId = viewModel.EmployeeId;
                    loan.LoanAmount = viewModel.LoanAmount;
                    loan.InterestRate = viewModel.InterestRate;
                    loan.LoanTerm = viewModel.LoanTerm;
                    loan.IssueDate = viewModel.IssueDate;
                    loan.CurrentBalance = viewModel.LoanAmount * viewModel.InterestRate;

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
            return _context.Loan.Any(e => e.LoanId == id);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var loans = await _context.Loan.FindAsync(id);

            if (loans == null)
            {
                //return Json(new { success = false, message = "Сотрудник не найден" }); // Возвращаем JSON с ошибкой
                return NotFound();
            }

            _context.Loan.Remove(loans);
            await _context.SaveChangesAsync();

            //return Json(new { success = true, message = "Сотрудник успешно удален" }); // Возвращаем JSON об успехе
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ProcessLoanOperation(int loanId, string operationType, decimal amount)
        {
            try
            {
                var loan = _context.Loan.Find(loanId);

                if (loan == null)
                {
                    return NotFound(); // Или другой обработчик ошибки
                }

                switch (operationType)
                {
                    case "IssueLoan":
                        // Проверка: можно ли выдать заём
                        if (loan.LoanStatusId != 1)
                        { //Предполагаем, что 1 - это "Новый"
                            TempData["ErrorMessage"] = "Заём уже выдан";
                            return RedirectToAction("Index");
                        }
                        // Логика выдачи займа
                        IssueLoan(loan, amount);
                        break;
                    case "Payment":
                        // Логика внесения платежа
                        ProcessPayment(loan, amount);
                        break;
                    // ... другие типы операций ...
                    default:
                        TempData["ErrorMessage"] = "Неизвестный тип операции.";
                        return RedirectToAction("Index");
                }

                _context.SaveChanges();

                return RedirectToAction("Index"); // Перенаправление обратно на страницу со списком займов
            }
            catch (Exception ex)
            {
                // Обработка ошибок (логирование, отображение сообщения пользователю)
                TempData["ErrorMessage"] = "Произошла ошибка: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // Пример реализации логики выдачи займа
        private void IssueLoan(Models.Loan loan, decimal amount)
        {
            // Проверка, что сумма выдачи соответствует сумме займа.
            if (loan.LoanAmount != amount)
            {
                throw new Exception("Сумма выдачи не соответствует сумме займа");
            }

            // 1. Создаем транзакцию (предполагаем, что у вас есть TransactionTypeId для "Выдача займа")
            var transaction = new LoanTransaction
            {
                LoanId = loan.LoanId,
                TransactionTypeId = 1, // Замените на ID типа "Выдача займа"
                TransactionDate = DateTime.Now,
                Amount = amount,
                NewCurrentBalance = amount  // CurrentBalance становится суммой займа
            };
            _context.LoanTransaction.Add(transaction);

            // 2. Меняем статус займа
            loan.LoanStatusId = 2; // Замените на ID статуса "Активный"
            loan.CurrentBalance = amount; // Устанавливаем текущий баланс равным сумме займа
        }

        // Пример реализации логики внесения платежа
        private void ProcessPayment(Models.Loan loan, decimal amount)
        {
            // 1. Создаем транзакцию (предполагаем, что у вас есть TransactionTypeId для "Внесение платежа")
            var transaction = new LoanTransaction
            {
                LoanId = loan.LoanId,
                TransactionTypeId = 2, // Замените на ID типа "Внесение платежа"
                TransactionDate = DateTime.Now,
                Amount = amount * -1,  // Отрицательное значение для уменьшения баланса
                NewCurrentBalance = loan.CurrentBalance - amount
            };
            _context.LoanTransaction.Add(transaction);

            // 2. Обновляем текущий баланс займа
            loan.CurrentBalance -= amount;

            // 3. Проверяем, погашен ли заём
            if (loan.CurrentBalance <= 0)
            {
                loan.LoanStatusId = 3; // Замените на ID статуса "Погашен"
                loan.CurrentBalance = 0; //Чтобы не было отрицательного баланса
            }

        }
    }
}
