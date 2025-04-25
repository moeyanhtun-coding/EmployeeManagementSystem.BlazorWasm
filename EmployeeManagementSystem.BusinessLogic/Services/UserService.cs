namespace EmployeeManagementSystem.BusinessLogic.Services;

public interface IUserService
{
    Task<List<UserModel>> GetAllUsersAsync();
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserModel>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }
}