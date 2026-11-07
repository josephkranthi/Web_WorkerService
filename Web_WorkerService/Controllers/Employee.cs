using Microsoft.AspNetCore.Mvc;
using Web_WorkerService.Models;

namespace Web_WorkerService.Controllers
{
    public class EmployeeController(EmployeeDbContext EmployeeDbContext) : Controller
    {  private readonly EmployeeDbContext _employeeDbContext = EmployeeDbContext;
        public IActionResult Index()
        {
            List<Employee> employees = [.. _employeeDbContext.Employees.Where(x => x.IsProcessed == false)];
            return View(employees);
        }
    }
}
