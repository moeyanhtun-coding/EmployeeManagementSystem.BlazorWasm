namespace EmployeeManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost("attendanceEmployee")]
        public async Task<ActionResult<BaseResponseModel>> AttendanceEmployee(AttendanceCreateRequestModel requestModel)
        {
            var res = await _attendanceService.AttendanceCreate(requestModel);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpGet ("attendanceList")]
        public async Task<ActionResult<BaseResponseModel>> AttendanceList()
        {
            var res = await _attendanceService.AttendanceList();
            return Ok(res);
        }
    }
}
