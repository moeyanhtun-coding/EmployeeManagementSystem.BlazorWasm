namespace EmployeeManagementSystem.Wasm.Services.Endpoints;

public static class EmployeeEndpoints
{
    public static string Create => "api/Employee/createEmployee";
    public static string GetList => "api/Employee/employeeList";
    public static string GetListPagination(int pageNo, int pageSize) =>  $"api/Employee/employeeList/{pageNo}/{pageSize}";
    public static string Edit(int id) => $"api/Employee/getEmployee/{id}";
    public static string Update(int id) => $"api/Employee/updateEmployee/{id}";
    public static string Delete(int id) => $"api/Employee/deleteEmployee/{id}";
    
}