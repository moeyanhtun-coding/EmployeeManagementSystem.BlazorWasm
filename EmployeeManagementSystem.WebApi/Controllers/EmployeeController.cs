using EmployeeManagementSystem.BusinessLogic.Services;
using EmployeeManagementSystem.Model.Entities;
using EmployeeManagementSystem.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet("employeeList")]
        public async Task<ActionResult<BaseResponseModel>> GetEmployeeList()
        {
            var employees = await employeeService.GetEmployeeListAsync();
            if (employees is null)
                return BadRequest(new BaseResponseModel { Data = null, IsSuccess = true, Message = "Employee Not Found" });
            return Ok(new BaseResponseModel { Data = employees, IsSuccess = true, Message = "Employee List" });
        }

        [HttpPost("createEmployee")]
        public async Task<ActionResult<BaseResponseModel>> CreateEmployee(EmployeeModel employeeModel)
        {
            var res = await employeeService.CreateEmployee(employeeModel);
            if (res > 0) return Ok(new BaseResponseModel { Data = employeeModel, IsSuccess = true, Message = "Employee Creation Successful" });
            return BadRequest(new BaseResponseModel { Data = null, IsSuccess = false, Message = "Employee Creation Failed" });
        }
    }
}
