﻿namespace EmployeeManagementSystem.Model.Entities
{
    [Table("Tbl_User")]
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
