﻿namespace EmployeeManagementSystem.Database.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected AppDbContext()
    {
    }

    public DbSet<EmployeeModel> Employees { get; set; } = null!;
    public DbSet<RoleModel> Roles { get; set; } = null!;
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<UserRoleModel> UserRoles { get; set; } = null!;
    public DbSet<RefreshTokenModel> RefreshToken { get; set; } = null!;
    public DbSet<AttendanceModel> Attendances { get; set; }
}
