using EmployeeManagementSystem.Model.Entities;
using EmployeeManagementSystem.Model.Models.Employee;

namespace EmployeeManagementSystem.Mapper;

public static class ChangeModel
{
    static string fullUlid = Ulid.NewUlid().ToString();
    public static EmployeeModel Change(this EmployeeRequestModel employeeRequestModel)
    {
        EmployeeModel employeeModel = new EmployeeModel()
        {
            EmployeeCode = string.Concat("EID-", fullUlid.AsSpan(0,10)),
            FirstName = employeeRequestModel.FirstName,
            LastName = employeeRequestModel.LastName,
            Email = employeeRequestModel.Email,
            PhoneNumber = employeeRequestModel.PhoneNumber,
            Address = employeeRequestModel.Address,
            DepartmentCode = employeeRequestModel.DepartmentCode,
            PositionCode = employeeRequestModel.PositionCode,
            DateOfJoining = employeeRequestModel.DateOfJoining,
            DateOfBirth = employeeRequestModel.DateOfBirth,
        };
        return employeeModel;
    }
}