using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model.Models
{
    public class ValidationErrorResponse
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
