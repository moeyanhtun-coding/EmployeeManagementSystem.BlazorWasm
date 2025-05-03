namespace EmployeeManagementSystem.Share
{
    public class CommonQuery
    {
        public static string GetUserDetail { get; } =
            @"SELECT Tbl_User.*, Tbl_Role.RoleName 
            FROM Tbl_User 
            JOIN Tbl_UserRole ON  Tbl_UserRole.UserId = Tbl_User.UserId
            JOIN Tbl_Role ON Tbl_Role.RoleId = Tbl_UserRole.RoleId 
            WHERE Tbl_User.UserId = @UserId";

        public static string GetUserDetailByRefreshTokenId { get; } =
            @"SELECT  Tbl_User.*, Tbl_Role.RoleName
            FROM Tbl_RefreshToken
            JOIN Tbl_User ON Tbl_RefreshToken.UserId = Tbl_User.UserId
            JOIN Tbl_UserRole ON Tbl_User.UserId = Tbl_UserRole.UserId
            JOIN Tbl_Role ON Tbl_UserRole.RoleId = Tbl_Role.RoleId 
            WHERE Tbl_RefreshToken.RefreshTokenId = @RefreshTokenId";

        public static string GetUserDetailList { get; } =
            @"SELECT  Tbl_User.*, Tbl_Role.RoleName
            FROM Tbl_RefreshToken
            JOIN Tbl_User ON Tbl_RefreshToken.UserId = Tbl_User.UserId
            JOIN Tbl_UserRole ON Tbl_User.UserId = Tbl_UserRole.UserId
            JOIN Tbl_Role ON Tbl_UserRole.RoleId = Tbl_Role.RoleId";

        public static string GetUserDetailByCode { get; } =
            @"SELECT Tbl_User.*, Tbl_Role.RoleId
            FROM Tbl_User 
            JOIN Tbl_UserRole ON  Tbl_UserRole.UserId = Tbl_User.UserId
            JOIN Tbl_Role ON Tbl_Role.RoleId = Tbl_UserRole.RoleId 
            WHERE Tbl_User.UserCode = @UserCode";
    }
}
