﻿namespace EmployeeManagementSystem.WebApi
{
    public static class ModularServices
    {
        public static IServiceCollection AddModularServices(this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
        {
            services.AddDbContext(configuration);
            services.AddDapperService(configuration);
            services.AddRepositories();
            services.AddServices();
            services.AddAuthService(builder);
            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnection")),
                ServiceLifetime.Transient,
                ServiceLifetime.Transient);
            return services;
        }
        public static IServiceCollection AddDapperService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDapperService, DapperService>(provider =>
                new DapperService(configuration.GetConnectionString("DbConnection")!));
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            return services;
        }

        public static IServiceCollection AddAuthService(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var secret = builder.Configuration.GetValue<string>("Jwt:Secret");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "moeYan",
                    ValidAudience = "moeYan",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                });

            return services;
        }
    }
}
