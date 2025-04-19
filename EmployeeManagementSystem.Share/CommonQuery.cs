using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Share
{
    public class CommonQuery
    {
        public static string GetUserList { get; } = "SELECT * FROM Tbl_User";
    }
}
