namespace EmployeeManagementSystem.BusinessLogic.Services
{
    public interface IUserService
    {
        Task<List<UserDetailModel>> GetUserList();
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<List<UserDetailModel>> GetUserList()
        {
            return await userRepository.GetUserList();
        }
    }
}
