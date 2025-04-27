namespace EmployeeManagementSystem.BusinessLogic.Services
{
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
            return await employeeRepository.UpdateEmployee(id, employeeModel);
        }

        public async Task<int> DeleteEmployee(int id)
        {
            return await employeeRepository.DeleteEmployee(id);
        }
    }
}
