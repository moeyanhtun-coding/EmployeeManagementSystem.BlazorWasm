using EmployeeManagementSystem.Database.Data;
using EmployeeManagementSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.BusinessLogic.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>> GetEmployeeList();
        Task<int> CreateEmployee(EmployeeModel employeeModel);
        Task<EmployeeModel> GetEmployeeById(int id);
        Task<int> UpdateEmployee(int id, EmployeeModel employeeModel);
        Task<int> DeleteEmployee(int id);
    }
    public class EmployeeRepository(AppDbContext _context) : IEmployeeRepository
    {
        public async Task<List<EmployeeModel>> GetEmployeeList()
        {
            return await _context.Employees.ToListAsync();
        }
        public async Task<int> CreateEmployee(EmployeeModel employeeModel)
        {
            await _context.Employees.AddAsync(employeeModel);
            return await _context.SaveChangesAsync();
        }

        public async Task<EmployeeModel> GetEmployeeById(int id)
        {
            var emoloyee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (emoloyee is null)
                return null;
            return emoloyee;
        }

        public async Task<int> UpdateEmployee(int id, EmployeeModel employeeModel)
        {
            var employee = await GetEmployeeById(id);

            employee.EmployeeCode = employeeModel.EmployeeCode;
            employee.FirstName = employeeModel.FirstName;
            employee.LastName = employeeModel.LastName;
            employee.Email = employeeModel.Email;
            employee.Address = employeeModel.Address;
            employee.PhoneNumber = employeeModel.PhoneNumber;
            employee.DepartmentCode = employeeModel.DepartmentCode;
            employee.PositionCode = employeeModel.PositionCode;
            employee.DateOfBirth = employeeModel.DateOfBirth;
            employee.DateOfJoining = employeeModel.DateOfJoining;

            _context.Entry(employee).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteEmployee(int id)
        {
            var employee = await GetEmployeeById(id);
            if (employee is null)
            {
                return 0;
            }
            else
            {
                _context.Entry(employee).State = EntityState.Deleted;
                return await _context.SaveChangesAsync();
            }
        }
    }
}
