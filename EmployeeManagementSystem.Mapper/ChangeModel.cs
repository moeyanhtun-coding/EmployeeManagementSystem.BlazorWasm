namespace EmployeeManagementSystem.Mapper;
public static class ChangeModel
{

    public static EmployeeModel Change(this EmployeeRequestModel employeeRequestModel)
    {
        EmployeeModel employeeModel = new EmployeeModel()
        {
            EmployeeCode = string.Concat("EID-", Ulid.NewUlid().ToString().AsSpan(5, 10)),
            FirstName = employeeRequestModel.FirstName,
            LastName = employeeRequestModel.LastName,
            Email = employeeRequestModel.Email,
            PhoneNumber = employeeRequestModel.PhoneNumber,
            Address = employeeRequestModel.Address,
            DepartmentCode = employeeRequestModel.DepartmentCode,
            PositionCode = employeeRequestModel.PositionCode,
            DateOfJoining = (DateTime)employeeRequestModel.DateOfJoining,
            DateOfBirth = (DateTime)employeeRequestModel.DateOfBirth,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
        return employeeModel;
    }

    public static UserModel Change(this RegisterModel registerModel) 
    {
        UserModel userModel = new UserModel()
        {
            UserCode = string.Concat("UID-", Ulid.NewUlid().ToString().AsSpan(5, 10)),
            UserName = registerModel.Name,
            Email = registerModel.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(registerModel.Password),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        return userModel;
    }

    public static AttendanceModel Change(this AttendanceCreateRequestModel model, string type ) 
    {
        AttendanceModel attendanceModel = new AttendanceModel()
        {
            AttendanceCode = string.Concat("AID-", Ulid.NewUlid().ToString().AsSpan(5, 10)),
            EmployeeCode = model.EmployeeCode,
            LogTime = DateTime.Now,
            Type = type
        };
        return attendanceModel;
    }
}