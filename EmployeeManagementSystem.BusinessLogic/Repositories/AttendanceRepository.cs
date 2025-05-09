using EmployeeManagementSystem.Model.Models.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.BusinessLogic.Repositories
{
    public interface IAttendanceRepository
    {
        Task<int> AttendanceCreate(AttendanceCreateRequestModel reqModel);
    }

    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AttendanceCreate(AttendanceCreateRequestModel reqModel)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeCode == reqModel.EmployeeCode);
            if (employee is null) 
            { 
            }
        }
    }
}
