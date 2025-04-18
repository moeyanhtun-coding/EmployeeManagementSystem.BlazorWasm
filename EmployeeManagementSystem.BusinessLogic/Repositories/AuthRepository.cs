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
    }

    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext context;

        public AuthRepository(AppDbContext context)
        {
            this.context = context;
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
                    Message = "Credential Do Not Match",
                    Data = null
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
            var newUser = new UserModel
            {
                UserName = model.Name,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
            return new BaseResponseModel
            {
                IsSuccess = true,
                Message = "User registered!",
                Data = newUser
            };
        }

        private async Task<UserModel> GetUserByEmail(string email)
        {
            
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
    }
}