namespace EmployeeManagementSystem.Model.Entities
{
    [Table("Tbl_UserRole")]
    public class UserRoleModel
    {
        [Key]
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
