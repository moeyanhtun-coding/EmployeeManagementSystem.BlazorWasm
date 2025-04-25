namespace EmployeeManagementSystem.BusinessLogic.Repositories;

public interface IUserRepository
{
    Task<List<UserModel>> GetAllUsersAsync();
}
public class UserRepository : IUserRepository
{
 private readonly   AppDbContext _context;

 public UserRepository(AppDbContext context)
 {
     _context = context;
 }

 public async Task<List<UserModel>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
}