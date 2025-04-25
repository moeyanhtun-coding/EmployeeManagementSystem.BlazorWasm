using EmployeeManagementSystem.Model.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.BusinessLogic.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserDetailModel>> GetUserList();
    }
    public class UserRepository : IUserRepository
    {
        private readonly IDapperService db;

        public UserRepository(IDapperService db)
        {
            this.db = db;
        }

        public async Task<List<UserDetailModel>> GetUserList()
        {
            var lst = await db.QueryAsync<UserDetailModel>(CommonQuery.GetUserDetailList);
            return lst;
        }
    }
}
