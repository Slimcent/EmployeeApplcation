using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Web.Models
{
    public class Employee
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age cannot be empty")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Account number cannot be empty")]
        public string AccountNumber { get; set; }
    }
}
