using EmployeeManagementSystem.Model.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
