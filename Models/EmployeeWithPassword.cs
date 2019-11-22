using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models
{
    public class EmployeeWithPassword
    {
        public Employee Employee { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
