namespace EmployeeManagementSystem.Model.Entities
{
    [Table("Tbl_Employee")]
    public class EmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string DepartmentCode { get; set; }
        public string PositionCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        
    }
}
