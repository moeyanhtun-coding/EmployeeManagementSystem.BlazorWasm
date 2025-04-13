using EmployeeManagementSystem.BusinessLogic.Repositories;
using EmployeeManagementSystem.Database.Data;
using EmployeeManagementSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.BusinessLogic.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetEmployeeListAsync();
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeModel>> GetEmployeeListAsync()
        {
            return await employeeRepository.GetEmployeeList();
        }
    }
}
