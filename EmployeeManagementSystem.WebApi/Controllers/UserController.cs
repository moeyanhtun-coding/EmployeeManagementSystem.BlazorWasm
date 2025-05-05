namespace EmployeeManagementSystem.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("getUserList")]
        public async Task<ActionResult<BaseResponseModel>> GetUserList()
        {
            try
            {
                var lst = await userService.GetUserList();
                return Ok(new BaseResponseModel
                {
                    IsSuccess = true,
                    Message = "User List",
                    Data = lst
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getUser/{userCode}")]
        public async Task<ActionResult<BaseResponseModel>> GetUserDetailByCode(string userCode)
        {
            try
            {
                var userDetail = await userService.GetUserDetailByCode(userCode);
                return Ok(new BaseResponseModel
                {
                    IsSuccess = true,
                    Message = "Get User Detail By Code",
                    Data = userDetail
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("userChangeRole/{userCode}")]
        public async Task<ActionResult<BaseResponseModel>> UserChangeRole(string userCode, int roleId)
        {
            try
            {
                var userRoleDetail = await userService.UserChangeRole(userCode, roleId);
                return Ok(new BaseResponseModel
                {
                    IsSuccess = true,
                    Message = "User Role Changed",
                    Data = userRoleDetail
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}