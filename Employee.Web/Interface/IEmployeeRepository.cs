using System;
using System.Collections.Generic;
using Employee.Web.Models;

namespace Employee.Web
{
    public interface IEmployeeRepository
    {
        IEnumerable<Models.Employee> GetAll();
        Models.Employee GetEmployee(Guid id);
        void CreateEmployee(Models.Employee employee);
    }
}