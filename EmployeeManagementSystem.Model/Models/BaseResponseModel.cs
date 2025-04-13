using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model.Models
{
    public class BaseResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
