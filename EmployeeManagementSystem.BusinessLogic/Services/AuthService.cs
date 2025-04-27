

namespace EmployeeManagementSystem.BusinessLogic.Services
{
    public interface IAuthService
    {
        Task<BaseResponseModel> LoginUserAsync(LoginModel model);
        Task<BaseResponseModel> RegisterUserAsync(RegisterModel model);
        Task AddRefreshTokenAsync(RefreshTokenModel model);
        Task<RefreshTokenModel> GetRefreshTokenModelAsync(string refreshToken);
        Task<UserDetailModel> GetUserDetailModelAsync(int refreshTokenId);
    }

    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public async Task AddRefreshTokenAsync(RefreshTokenModel model)
        {
            await authRepository.AddRefreshTokenAsync(model);
        }

        public async Task<RefreshTokenModel> GetRefreshTokenModelAsync(string refreshToken)
        {
            return await authRepository.GetRefreshTokenModelAsync(refreshToken);
        }

        public async Task<UserDetailModel> GetUserDetailModelAsync(int refreshTokenId)
        {
            return await authRepository.GetUserDetailModelAsync(refreshTokenId);
        }


        public async Task<BaseResponseModel> LoginUserAsync(LoginModel model)
        {
            return await authRepository.LoginUserAsync(model);
        }

        public async Task<BaseResponseModel> RegisterUserAsync(RegisterModel model)
        {
            return await authRepository.RegisterUserAsync(model);
        }
    }
}