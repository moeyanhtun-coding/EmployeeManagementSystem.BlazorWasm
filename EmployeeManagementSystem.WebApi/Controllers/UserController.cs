using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponseModel>> GetUserListAsync()
        {
            var res = await _userService.GetAllUsersAsync();
            if (res is null) return BadRequest();
            return Ok(new BaseResponseModel
            {
                IsSuccess = true,
                Message = "User list retrieved",
                Data = res
            });
        }
    }
}
