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
        Task<int> CreateEmployee(EmployeeModel employeeModel);
        Task<EmployeeModel> GetEmployeeById(int id);
        Task<int> UpdateEmployee(int id, EmployeeModel employeeModel);
        Task<int> DeleteEmployee(int id);
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

        public async Task<EmployeeModel> GetEmployeeById(int id)
        {
            return await employeeRepository.GetEmployeeById(id);
        }

        public async Task<int> CreateEmployee(EmployeeModel employeeModel)
        {
            return await employeeRepository.CreateEmployee(employeeModel);
        }

        public async Task<int> UpdateEmployee(int id, EmployeeModel employeeModel)
        {
            return await employeeRepository.UpdateEmployee(id, employeeModel);
        }

        public async Task<int> DeleteEmployee(int id)
        {
            return await employeeRepository.DeleteEmployee(id);
        }
    }
}
