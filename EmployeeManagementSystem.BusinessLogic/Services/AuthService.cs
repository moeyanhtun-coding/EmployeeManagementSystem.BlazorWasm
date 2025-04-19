namespace EmployeeManagementSystem.BusinessLogic.Services
{
    public interface IAuthService
    {
        Task<BaseResponseModel> LoginUserAsync(LoginModel model);
        Task<BaseResponseModel> RegisterUserAsync(RegisterModel model);
        Task AddRefreshTokenAsync(RefreshTokenModel model);
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
