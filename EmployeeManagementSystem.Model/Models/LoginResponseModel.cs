using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model.Models
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public long TokenExpired { get; set; } 
    }
}
