namespace EmployeeManagementSystem.BusinessLogic.Services;

public interface IEmployeeService
{
    Task<List<EmployeeModel>> GetEmployeeListAsync();
    Task<int> CreateEmployee(EmployeeRequestModel employeeModel);
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

    public async Task<int> CreateEmployee(EmployeeRequestModel employeeModel)
    {
        return await employeeRepository.CreateEmployee(employeeModel);
    }

    public async Task<int> UpdateEmployee(int id, EmployeeModel employeeModel)
    {
        var employee = await employeeRepository.GetEmployeeById(id);
        if (employee is null) return 0;

        employee.FirstName = employeeModel.FirstName;
        employee.LastName = employeeModel.LastName;
        employee.Email = employeeModel.Email;
        employee.PhoneNumber = employeeModel.PhoneNumber;
        employee.Address = employeeModel.Address;
        employee.DepartmentCode = employeeModel.DepartmentCode;
        employee.PositionCode = employeeModel.PositionCode;
        employee.UpdatedAt = DateTime.Now;

        return await employeeRepository.UpdateEmployee(employee);
    }

    public async Task<int> DeleteEmployee(int id)
    {
        var employee = await employeeRepository.GetEmployeeById(id);
        return await employeeRepository.DeleteEmployee(employee);
    }
}