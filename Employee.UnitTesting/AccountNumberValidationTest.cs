using Employee.Web.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Employee.UnitTesting
{
    public class AccountNumberValidationTest
    {
        private readonly AccountNumberValidation _validation;

        public AccountNumberValidationTest()
        {
            _validation = new AccountNumberValidation();
        }

        [Fact]
        public void IsValid_ValidAccountNumber_ReturnsTrue() =>
            Assert.True(_validation.IsValid("123-4567891234-12"));

        [Fact]
        public void IsValid_ValidAccountNumber_ReturnsFalse() =>
            Assert.False(_validation.IsValid("123456789233445"));

        [Fact]
        public void IsValid_AccountNumberFirstPartIsWrong_ReturnsFalse() =>
            Assert.False(_validation.IsValid("1233-4567891234-12"));

        [Theory]
        [InlineData("1234-4567891234-12")]
        [InlineData("12-4567891234-12")]
        public void IsValid_AccountNumberFirstPartWrong_ReturnsFalse(string accountNumber) =>
            Assert.False(_validation.IsValid(accountNumber));

        [Theory]
        [InlineData("123-45678912341-12")]
        [InlineData("123-456789123-12")]
        public void IsValid_AccountNumberMiddlePartWrong_ReturnsFalse(string accountNumber) =>
            Assert.False(_validation.IsValid(accountNumber));

        [Theory]
        [InlineData("1234-4567891234-1")]
        [InlineData("12-4567891234-123")]
        public void IsValid_AccountNumberLastPartWrong_ReturnsFalse(string accountNumber) =>
            Assert.False(_validation.IsValid(accountNumber));

        [Theory]
        [InlineData("123-4567891234=12")]
        [InlineData("123+4567891234-12")]
        [InlineData("123+4567891234=12")]
        public void IsValid_InvalidDelimeters_ThrowsArgumentException(string accountNumber) =>
            Assert.Throws<ArgumentException>(() => _validation.IsValid(accountNumber));
    }
}
