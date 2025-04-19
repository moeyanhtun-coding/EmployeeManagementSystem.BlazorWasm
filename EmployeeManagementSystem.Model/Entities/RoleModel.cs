﻿namespace EmployeeManagementSystem.Model.Entities
{
    [Table("Tbl_Role")] 
    public class RoleModel
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
