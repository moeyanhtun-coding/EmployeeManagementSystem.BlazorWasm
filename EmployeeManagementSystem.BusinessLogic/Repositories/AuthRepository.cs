using EmployeeManagementSystem.Database.Data;
using EmployeeManagementSystem.Model.Entities;
using EmployeeManagementSystem.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.BusinessLogic.Repositories
{
    public interface IAuthRepository
    {
        Task<BaseResponseModel> LoginUserAsync(LoginModel model);
        Task<BaseResponseModel> RegisterUserAsync(RegisterModel model);
        Task AddRefreshTokenAsync(RefreshTokenModel model);
    }

    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext context;

        public AuthRepository(AppDbContext context)
        {
            this.context = context;
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
            return new BaseResponseModel
            {
                IsSuccess = true,
                Message = "Login Successful",
                Data = user
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

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {

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
                        RoleId = 2
                    };
                    context.UserRoles.Add(userRole);
                    await context.SaveChangesAsync();
                    #endregion

                    return new BaseResponseModel
                    {
                        IsSuccess = true,
                        Message = "User registered!",
                        Data = newUser
                    };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    return new BaseResponseModel
                    {
                        IsSuccess = false,
                        Message = $"Registration failed: {ex.Message}",
                        Data = null
                    };
                }
            }


        }

        private async Task<UserModel> GetUserByEmail(string email)
        {

            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
    }
}