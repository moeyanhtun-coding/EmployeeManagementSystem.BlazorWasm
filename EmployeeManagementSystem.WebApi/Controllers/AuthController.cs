﻿namespace EmployeeManagementSystem.WebApi.Controllers
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
            var user = res.Data as UserModel;
            var token = GenerateJwtToken(user!.UserName, false);
            var refreshToken = GenerateJwtToken(user.UserName, true);
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
            var user = res.Data as UserModel;
            var token = GenerateJwtToken(user!.UserName, false);
            var refreshToken = GenerateJwtToken(user.UserName, true);
            await authService.AddRefreshTokenAsync(new RefreshTokenModel
            {
                UserId = user.UserId,
                RefreshToken = refreshToken,
            });
            return Ok(new LoginResponseModel
            {
                Token = token,
                RefreshToken = refreshToken,
                TokenExpired = DateTimeOffset.UtcNow.AddMinutes(20).ToUnixTimeSeconds()
            });
        }

        [HttpGet("loginByRefreshToken")]
        public ActionResult<LoginResponseModel> LoginByRefreshToken(string refreshToken)
        {
            var secret = _configuration.GetValue<string>("Jwt:RefreshTokenSecret");
            var claimsPrincipal = GetClaimPrincipalFromToken(refreshToken, secret);
            if (claimsPrincipal is null)
            {
                return new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }

            var userName = claimsPrincipal.FindFirstValue(ClaimTypes.Name);
            var newToken = GenerateJwtToken(userName, false);
            var newRefreshToken = GenerateJwtToken(userName, true);
            return new LoginResponseModel
            {
                Token = newToken,
                RefreshToken = newRefreshToken,
                TokenExpired = DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds()
            };
        }

        private ClaimsPrincipal GetClaimPrincipalFromToken(string refreshToken, string? secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            try
            {
                var principal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = "moeYan",
                    ValidateIssuer = true,
                    ValidIssuer = "moeYan",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                }, out var validatedToken);
                return principal;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        private string GenerateJwtToken(string username, bool isRefreshToken)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin"),
             };
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