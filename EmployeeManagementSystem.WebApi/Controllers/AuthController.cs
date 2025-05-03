namespace EmployeeManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginModel model)
        {
            var res = await authService.LoginUserAsync(model);
            if (!res.IsSuccess) return BadRequest(res);
            var user = res.Data as UserDetailModel;
            var token = GenerateJwtToken(user, false);
            var refreshToken = GenerateJwtToken(user, true);
            await authService.AddRefreshTokenAsync(new RefreshTokenModel
            {
                UserCode = user.UserCode,
                RefreshToken = refreshToken,
            });
            return Ok(new LoginResponseModel
            {
                Token = token,
                RefreshToken = refreshToken,
                TokenExpired = DateTimeOffset.UtcNow.AddMinutes(20).ToUnixTimeSeconds()
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponseModel>> Register([FromBody] RegisterModel model)
        {
            var res = await authService.RegisterUserAsync(model);
            if (!res.IsSuccess) return BadRequest(res);

            var user = res.Data as UserDetailModel;
            Console.WriteLine(JsonConvert.SerializeObject(user));
            var token = GenerateJwtToken(user, false);
            var refreshToken = GenerateJwtToken(user, true);
            await authService.AddRefreshTokenAsync(new RefreshTokenModel
            {
                UserCode = user.UserCode,
                RefreshToken = refreshToken,
            });
            return Ok(new LoginResponseModel
            {
                Token = token,
                RefreshToken = refreshToken,
                TokenExpired = DateTimeOffset.UtcNow.AddMinutes(20).ToUnixTimeSeconds()
            });
        }

        [HttpGet("loginByRefreshToken/{refreshToken}")]
        public async Task<ActionResult<LoginResponseModel>> LoginByRefreshToken(string refreshToken)
        {
            try
            {
                var refreshTokenDetail = await authService.GetRefreshTokenModelAsync(refreshToken);
                if (refreshTokenDetail == null)
                {
                    return Unauthorized("Invalid refresh token.");
                }

                var userDetail = await authService.GetUserDetailModelAsync(refreshTokenDetail.RefreshTokenId);
                if (userDetail == null)
                {
                    return Unauthorized("User not found.");
                }

                var newToken = GenerateJwtToken(userDetail, false);
                var newRefreshToken = GenerateJwtToken(userDetail, true);
                await authService.AddRefreshTokenAsync(new RefreshTokenModel
                {
                    UserCode = userDetail.UserCode  ,
                    RefreshToken = newRefreshToken,
                });

                return Ok(new LoginResponseModel
                {
                    Token = newToken,
                    RefreshToken = newRefreshToken,
                    TokenExpired = DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds()
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, ex.ToString());
            }

        }

        private string GenerateJwtToken(UserDetailModel userDetail, bool isRefreshToken)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userDetail.UserName),
                new Claim(ClaimTypes.Role, userDetail.RoleName),
                new Claim(ClaimTypes.Email, userDetail.Email),
                new Claim(ClaimTypes.NameIdentifier, userDetail.UserCode)
             };
            Console.WriteLine(claims.Length);
            string secret =
                _configuration.GetValue<string>($"Jwt:{(isRefreshToken ? "RefreshTokenSecret" : "Secret")}")!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "moeYan",
                audience: "moeYan",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(isRefreshToken ? 30 : 20),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}