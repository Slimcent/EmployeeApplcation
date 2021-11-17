using Employee.Web.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using Employee.Web.Models;
using System.Threading.Tasks;

namespace Employee.Web.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;
        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.Employee> GetAll() => _context.Employees.ToList();

        public Models.Employee GetEmployee(Guid id) => _context.Employees
            .SingleOrDefault(e => e.Id.Equals(id));

        public void CreateEmployee(Models.Employee employee)
        {
            _context.Add(employee);
            _context.SaveChanges();
        }
    }
}
