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

        [HttpGet("getEmployee/{id}")]
        public async Task<ActionResult<BaseResponseModel>> EditEmployee(int id)
        {
            var res = await employeeService.GetEmployeeById(id);
            if (res is null)
                return BadRequest(new BaseResponseModel { Data = null, IsSuccess = false, Message = "Employee Not Found" });
            return Ok(new BaseResponseModel { Data = res, IsSuccess = true, Message = "Employee Found" });
        }

        [HttpPatch("updateEmployee/{id}")]
        public async Task<ActionResult<BaseResponseModel>> UpdateEmployee( int id, EmployeeModel employee)
        {
            var res = await employeeService.UpdateEmployee(id, employee);
            if (res is 0)
                return BadRequest(new BaseResponseModel { Data = employee, IsSuccess = false, Message = "Employee Updating Failed" });
            return Ok(new BaseResponseModel { Message = "Employee Updating Successful", IsSuccess = true, Data = employee });
        }
    }
}

