namespace EmployeeManagementSystem.BusinessLogic.Services
{
    public interface IUserService
    {
        Task<List<UserDetailModel>> GetUserList();
        Task<UserDetailByCodeModel> GetUserDetailByCode(string userCode);
        Task<UserRoleModel> UserChangeRole(string userCode, UserChangeRoleRequestModel req);
        Task<int> UserDelete(string userCode); 
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserDetailByCodeModel> GetUserDetailByCode(string userCode)
        {
           return await userRepository.GetUserDetailByCode(userCode);
        }

        public async Task<List<UserDetailModel>> GetUserList()
        {
            return await userRepository.GetUserList();
        }

        public async Task<UserRoleModel> UserChangeRole(string userCode, UserChangeRoleRequestModel req)
        {
           return await userRepository.UserChangeRole(userCode, req);
        }

        public async Task<int> UserDelete(string userCode)
        {
            return await userRepository.UserDelete(userCode);
        }
    }
}
