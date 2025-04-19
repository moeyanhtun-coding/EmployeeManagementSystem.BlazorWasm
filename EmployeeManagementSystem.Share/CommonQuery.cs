namespace EmployeeManagementSystem.Share
{
    public class CommonQuery
    {
        public static string GetUserDetail { get; } = @"Select Tbl_User.*, Tbl_Role.RoleName 
                                                        From Tbl_User 
                                                        Join Tbl_UserRole on  Tbl_UserRole.UserId = Tbl_User.UserId
                                                        Join Tbl_Role on Tbl_Role.RoleId = Tbl_UserRole.RoleId 
                                                        Where Tbl_User.UserId = @UserId";
    }
}
