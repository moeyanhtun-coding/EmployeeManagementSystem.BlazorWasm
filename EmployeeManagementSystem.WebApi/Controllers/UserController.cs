namespace EmployeeManagementSystem.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet("getUserList")]
        public async Task<ActionResult<BaseResponseModel>> GetUserList()
        {
            try
            {
                var lst = await userRepository.GetUserList();
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
                var  userDetail = await userRepository.GetUserDetailByCode(userCode);
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
    }
}
