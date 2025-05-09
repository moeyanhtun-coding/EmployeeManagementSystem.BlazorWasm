using EmployeeManagementSystem.BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        [HttpPost("attendanceEmployee/{employeeCode}")]
        public async Task<ActionResult<BaseResponseModel>> AttendanceEmployee(string employeeCode)
        {
            var res = await employeeService.AttendanceEmployee(employeeCode);
        }
    }
}
