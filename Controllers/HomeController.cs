using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;

namespace SpendSmart.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SpendSmartDbContext _context;

    public HomeController(ILogger<HomeController> logger, SpendSmartDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Expenses()
    {
        var allExpenses = _context.Expenses.ToList();
        var totalExpenses = allExpenses.Sum(e => e.Value);
        ViewBag.TotalExpenses = totalExpenses;
        return View(allExpenses);
    }

    public IActionResult AddEditExpense(int? id)
    {
        if (id != null)
        {
            var expense = _context.Expenses.Find(id);
            if (expense != null)
            {
                return View(expense);
            }
        }
        return View();
    }

    public IActionResult AddOrEditExpenseForm(Expense model)
    {
        if (model.Id == 0)
        {
            _context.Expenses.Add(model);
        }
        else
        {
            _context.Expenses.Update(model);
        }
        _context.SaveChanges();
        return RedirectToAction("Expenses");
    }

    public IActionResult DeleteExpense(int id)
    {
        var expense = _context.Expenses.Find(id);
        if (expense != null)
        {
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
        }
        return RedirectToAction("Expenses");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
