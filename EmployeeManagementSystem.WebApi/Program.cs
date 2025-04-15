using EmployeeManagementSystem.WebApi;
using Microsoft.OpenApi.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7188",
                "http://localhost:5240").AllowAnyMethod().AllowAnyHeader();
        });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
     swagger =>
     {
         swagger.SwaggerDoc("v1", new OpenApiInfo
         {
             Version = "v1",
             Title = "ASP.NET Core 8 Web API",
             Description = "ASP.NET Core 8 Web API with JWT Authentication"
         });
         swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
             Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
             Name = "Authorization",
             In = ParameterLocation.Header,
             Type = SecuritySchemeType.ApiKey,
             BearerFormat = "JWT",
             Scheme = "Bearer"
         });
         swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
         });
     }
    );

builder.Services.AddModularServices(builder.Configuration, builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
