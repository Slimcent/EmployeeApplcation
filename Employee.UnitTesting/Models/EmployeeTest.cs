using Xunit;


namespace Employee.UnitTesting.Models
{
    public class EmployeeTest
    {
        [Fact]
        public void CanChangeEmployeeName()
        {
            // Arrange
            var e = new Employee.Web.Models.Employee { Name = "Test", Age = 20, AccountNumber = "123-2345676567-43" };
            // Act
            e.Name = "New Name";
            //Assert
            Assert.Equal("New Name", e.Name);
        }

        [Fact]
        public void CanChangeEmployeeAge()
        {
            // Arrange
            var e = new Employee.Web.Models.Employee { Name = "Test", Age = 20, AccountNumber = "123-2345676567-43" };
            // Act
            e.Age = 21;
            //Assert
            Assert.Equal(21, e.Age);
        }

        [Fact]
        public void CanChangeEmployeeAccountNumber()
        {
            // Arrange
            var e = new Employee.Web.Models.Employee { Name = "Test", Age = 20, AccountNumber = "123-2345676567-43" };
            // Act
            e.AccountNumber = "123-2349976567-43";
            //Assert
            Assert.Equal("123-2349976567-43", e.AccountNumber);
        }

    }
}
