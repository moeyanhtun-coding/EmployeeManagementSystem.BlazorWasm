using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model.Models
{
    public class LoginModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(6), MaxLength(20)] 
        public string Password { get; set; }
    }
}
