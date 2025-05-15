namespace EmployeeManagementSystem.BusinessLogic.Repositories;

public interface IEmployeeRepository
{
    Task<List<EmployeeModel>> GetEmployeeList();

    Task<EmployeeListResponseModel> GetEmployeeList(int pageNo, int pageSize);
    Task<EmployeeModel> GetEmployeeById(int id);
    Task<int> CreateEmployee(EmployeeRequestModel employeeModel);
    Task<int> UpdateEmployee(EmployeeModel employeeModel);
    Task<int> DeleteEmployee(EmployeeModel employeeModel);
}

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _dbContext;

    public EmployeeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<EmployeeModel>> GetEmployeeList()
    {
        return await _dbContext.Employees.AsNoTracking().ToListAsync();
    }

    public async Task<EmployeeListResponseModel> GetEmployeeList(int pageNo, int pageSize)
    {
        var query = _dbContext.Employees.AsNoTracking();
        var totalCount = await query.CountAsync();
        var pageCount = totalCount / pageSize;
        if (totalCount % pageSize > 0)
            pageCount++;
        var response = new EmployeeListResponseModel()
        {
            PageSetting = new PageSettingModel(pageNo, pageSize, totalCount, pageCount),
            DataList = await query.ToListAsync(),
        };
        return response;
    }

    public async Task<int> CreateEmployee(EmployeeRequestModel employeeModel)
    {
        await _dbContext.Employees.AddAsync(employeeModel.Change());
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<EmployeeModel> GetEmployeeById(int id)
    {
        return await _dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.EmployeeId == id);
    }

    public async Task<int> UpdateEmployee(EmployeeModel employeeModel)
    {
        _dbContext.Entry(employeeModel).State = EntityState.Modified;
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<int> DeleteEmployee(EmployeeModel employeeModel)
    {
        _dbContext.Entry(employeeModel).State = EntityState.Deleted;
        return await _dbContext.SaveChangesAsync();
    }
}