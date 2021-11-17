using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee.Web.Models;

namespace Employee.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _repo;

        public EmployeesController(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var employees = _repo.GetAll();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,AccountNumber,Age")] Models.Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            _repo.CreateEmployee(employee);
            return RedirectToAction(nameof(Index));
        }
    }
}
