﻿using EmployeeManagementSystem.Database.Data;
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
    }
    public class EmployeeRepository(AppDbContext _context) : IEmployeeRepository
    {
        public async Task<List<EmployeeModel>> GetEmployeeList()
        {
          return await _context.Employees.ToListAsync();
        }
    }
}
