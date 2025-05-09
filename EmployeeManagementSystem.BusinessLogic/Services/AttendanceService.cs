using EmployeeManagementSystem.Model.Models.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.BusinessLogic.Services
{
    public interface IAttendanceService
    {
        Task<BaseResponseModel> AttendanceCreate(AttendanceCreateRequestModel reqModel);
        Task<BaseResponseModel> AttendanceList();

    }

    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            this.attendanceRepository = attendanceRepository;
        }

        public async Task<BaseResponseModel> AttendanceCreate(AttendanceCreateRequestModel reqModel)
        {
            return await attendanceRepository.AttendanceCreate(reqModel);
        }

        public async Task<BaseResponseModel> AttendanceList()
        {
            return await attendanceRepository.AttendanceList();
        }
    }
}
