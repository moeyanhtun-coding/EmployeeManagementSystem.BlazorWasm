namespace EmployeeManagementSystem.BusinessLogic.Repositories
{
    public interface IAuthRepository
    {
        Task<BaseResponseModel> LoginUserAsync(LoginModel model);
        Task<BaseResponseModel> RegisterUserAsync(RegisterModel model);
        Task AddRefreshTokenAsync(RefreshTokenModel model);
        Task<RefreshTokenModel> GetRefreshTokenModelAsync(string refreshToken);
        Task<UserDetailModel> GetUserDetailModelAsync(int refreshTokenId);
    }

    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext context;
        private readonly IDapperService dapperService;

        public AuthRepository(AppDbContext context, IDapperService dapperService)
        {
            this.context = context;
            this.dapperService = dapperService;
        }

        public async Task AddRefreshTokenAsync(RefreshTokenModel model)
        {
            var refreshToken = await context.RefreshToken.FirstOrDefaultAsync(x => x.UserId == model.UserId);
            if (refreshToken is null)
            {
                await context.RefreshToken.AddAsync(model);
                await context.SaveChangesAsync();
            }
            else
            {
                refreshToken.RefreshToken = model.RefreshToken;
                context.Entry(refreshToken).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task<RefreshTokenModel> GetRefreshTokenModelAsync(string refreshToken)
        {
            var tokenDetail = await context.RefreshToken.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            return tokenDetail;
        }

        public async Task<UserDetailModel> GetUserDetailModelAsync(int refreshTokenId)
        {
            var userDetail = await dapperService.QueryFirstOrDefaultAsync<UserDetailModel>(CommonQuery.GetUserDetailByRefreshTokenId, new { RefreshTokenId = refreshTokenId });
            return userDetail;
        }

        public async Task<BaseResponseModel> LoginUserAsync(LoginModel model)
        {
            var user = await GetUserByEmail(model.Email);
            if (user is null)
                return new BaseResponseModel
                {
                    IsSuccess = false,
                    Message = "User Not Found",
                    Data = null
                };
            var checkPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            if (!checkPassword)
                return new BaseResponseModel
                {
                    IsSuccess = false,
                };
            #region GetUserDetail
            var userDetail = await dapperService.QueryFirstOrDefaultAsync<UserDetailModel>(CommonQuery.GetUserDetail,new { UserId = user.UserId });
            #endregion
            return new BaseResponseModel
            {
                IsSuccess = true,
                Message = "Login Successful",
                Data = userDetail
            };
        }

        public async Task<BaseResponseModel> RegisterUserAsync(RegisterModel model)
        {
            var user = await GetUserByEmail(model.Email!);
            if (user is not null)

                return new BaseResponseModel
                {
                    IsSuccess = false,
                    Message = "User already exists!",
                    Data = user
                };

            #region Add User
            var newUser = new UserModel
            {
                UserName = model.Name,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
            #endregion

            #region Add UserRole
            var userRole = new UserRoleModel
            {
                UserId = newUser.UserId,
                RoleId = 1
            };
            context.UserRoles.Add(userRole);
            await context.SaveChangesAsync();
            #endregion

            #region GetUserDetail
            var userDetail = await dapperService.QueryFirstOrDefaultAsync<UserDetailModel>(CommonQuery.GetUserDetail, new { UserId = user.UserId });
            #endregion
            return new BaseResponseModel
            {
                IsSuccess = true,
                Message = "User registered!",
                Data = userDetail
            };
        }

        private async Task<UserModel> GetUserByEmail(string email)
        {

            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
    }
}