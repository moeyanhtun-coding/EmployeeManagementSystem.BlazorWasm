namespace EmployeeManagementSystem.BusinessLogic.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserDetailModel>> GetUserList();
        Task<UserDetailByCodeModel> GetUserDetailByCode(string userCode);
    }
    public class UserRepository : IUserRepository
    {
        private readonly IDapperService db;
        private readonly AppDbContext context;

        public UserRepository(IDapperService db, AppDbContext context)
        {
            this.db = db;
            this.context = context;
        }

        public async Task<List<UserDetailModel>> GetUserList()
        {
            var lst = await db.QueryAsync<UserDetailModel>(CommonQuery.GetUserDetailList);
            return lst;
        }

        public async Task<UserDetailByCodeModel> GetUserDetailByCode(string userCode)
        {
            return  await db.QueryFirstOrDefaultAsync<UserDetailByCodeModel>(CommonQuery.GetUserDetailByCode, new { UserCode = userCode });
        }
    }
}
