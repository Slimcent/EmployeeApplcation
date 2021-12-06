using Employee.Web;
using Employee.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Employee.UnitTesting.Controllers
{
    public class EmployeesControllerTest
    {
        private readonly Mock<IEmployeeRepository> _mockRepo;
        private readonly EmployeesController _controller;

        public EmployeesControllerTest()
        {
            _mockRepo = new Mock<IEmployeeRepository>();
            _controller = new EmployeesController(_mockRepo.Object);
        }

        [Fact]
        public void Index_Action_ReturnsAViewResult()
        {
            var result = _controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_Action_ReturnsExactNumberOfEmployees()
        {
            _mockRepo.Setup(repo => repo.GetAll())
                .Returns(new List<Employee.Web.Models.Employee>() { new Employee.Web.Models.Employee(), new Employee.Web.Models.Employee(), new Employee.Web.Models.Employee() });

            var result = _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var employees = Assert.IsType<List<Employee.Web.Models.Employee>>(viewResult.Model);

            Assert.Equal(3, employees.Count);
        }

        [Fact]
        public void Create_Action_ReturnsAViewResult()
        {
            var result = _controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Action_InvalidModelValidtion_ReturnsView()
        {
            _controller.ModelState.AddModelError("Name", "Name cannot be empty");

            var employee = new Employee.Web.Models.Employee { Age = 20, AccountNumber = "221-2345432676-65" };

            var result = _controller.Create(employee);

            var viewResult = Assert.IsType<ViewResult>(result);
            var testEmployee = Assert.IsType<Employee.Web.Models.Employee>(viewResult.Model);

            Assert.Equal(employee.AccountNumber, testEmployee.AccountNumber);
            Assert.Equal(employee.Age, testEmployee.Age);
        }

        [Fact]
        public void Create_Action_InvalidModelState_CreateEmployeeNeverExecutes()
        {
            _controller.ModelState.AddModelError("Name", "Name cannot be Empty");

            var employee = new Employee.Web.Models.Employee { Age = 25 };
            _controller.Create(employee);

            _mockRepo.Verify(x => x.CreateEmployee(It.IsAny<Employee.Web.Models.Employee>()), Times.Never);
        }

        [Fact]
        public void Create_Action_ValidModelState_CreateEmployeeExecutesOnce()
        {
            Employee.Web.Models.Employee emp = null;

            _mockRepo.Setup(e => e.CreateEmployee(It.IsAny<Employee.Web.Models.Employee>()))
                .Callback<Employee.Web.Models.Employee>(x => emp = x);

            var employee = new Employee.Web.Models.Employee 
            {
                Name = "Test Employee",
                Age = 25,
                AccountNumber = "222-2345432346-33"
            };

            _controller.Create(employee);
            _mockRepo.Verify(x => x.CreateEmployee(It.IsAny<Employee.Web.Models.Employee>()), Times.Once);

            Assert.Equal(emp.Name, employee.Name);
            Assert.Equal(emp.Age, employee.Age);
            Assert.Equal(emp.AccountNumber, employee.AccountNumber);
        }

        [Fact]
        public void Create_Action_ValidModelState_CreateEmployeeRedirectsToIndexAction()
        {
            var employee = new Employee.Web.Models.Employee
            {
                Name = "Test Employee",
                Age = 25,
                AccountNumber = "222-2345432346-33"
            };

           var result = _controller.Create(employee);
            var redirectToIndexAction = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToIndexAction.ActionName);
        }

    }
}
