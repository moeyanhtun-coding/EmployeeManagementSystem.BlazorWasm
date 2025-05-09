using EmployeeManagementSystem.Model.Models.Attendance;

namespace EmployeeManagementSystem.BusinessLogic.Repositories;

public interface IAttendanceRepository
{
    Task<BaseResponseModel> AttendanceCreate(AttendanceCreateRequestModel reqModel);
    Task<BaseResponseModel> AttendanceList();
}

public class AttendanceRepository : IAttendanceRepository
{
    private readonly AppDbContext _context;

    public AttendanceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BaseResponseModel> AttendanceCreate(AttendanceCreateRequestModel reqModel)
    {
        try
        {
            var today = DateTime.Today;
            var noon = today.AddHours(12);
            var tomorrow = today.AddDays(1);
            var now = DateTime.Now;
            string type;

            var employee = await _context.Employees
                .FirstOrDefaultAsync(x => x.EmployeeCode == reqModel.EmployeeCode);

            if (employee is null)
            {
                return new BaseResponseModel()
                {
                    IsSuccess = false,
                    Message = "Employee Not Found",
                    Data = null
                };
            }

            var hasCheckIn = await _context.Attendances
                .Where(x => x.EmployeeCode == reqModel.EmployeeCode && x.LogTime >= today && x.LogTime < tomorrow && x.Type == "CheckIn")
                .FirstOrDefaultAsync();
            var hasCheckOut = await _context.Attendances
               .Where(x => x.EmployeeCode == reqModel.EmployeeCode && x.LogTime >= today && x.LogTime < tomorrow && x.Type == "CheckOut")
               .FirstOrDefaultAsync();
            if (now < noon)
            {
                if (hasCheckIn is not null)
                {
                    return new BaseResponseModel
                    {
                        IsSuccess = true,
                        Message = "Employee is Already CheckIn"
                    };
                }
                type = "CheckIn";
            }
            else
            {
                if (hasCheckOut is not null)
                {
                    return new BaseResponseModel
                    {
                        IsSuccess = true,
                        Message = "Employee is Already CheckOut"
                    };
                }
                type = "CheckOut";
            }

            await _context.Attendances.AddAsync(reqModel.Change(type));
            var result = await _context.SaveChangesAsync();
            var message = result > 0 ? "Attendance Create Successful" : "Attendance Create Failed";

            return new BaseResponseModel()
            {
                IsSuccess = true,
                Message = message
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public async Task<BaseResponseModel> AttendanceList()
    {
        var lst = await _context.Attendances.ToListAsync();
        return new BaseResponseModel
        {
            Data = lst,
            Message = "Attendance List",
            IsSuccess = true
        };
    }
}
