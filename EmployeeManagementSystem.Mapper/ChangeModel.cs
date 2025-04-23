using EmployeeManagementSystem.Model.Entities;
using EmployeeManagementSystem.Model.Models.Employee;

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
        };
        return employeeModel;
    }
}