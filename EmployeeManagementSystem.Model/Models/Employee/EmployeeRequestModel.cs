namespace EmployeeManagementSystem.Model.Models.Employee;

public class EmployeeRequestModel
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required, Phone]
    public string PhoneNumber { get; set; }
    [Required, MinLength(6), MaxLength(30)]
    public string Address { get; set; }
    [Required]
    public string DepartmentCode { get; set; }
    [Required]
    public string PositionCode { get; set; }
    [Required]
    public DateTime? DateOfBirth { get; set; }
    [Required]
    public DateTime? DateOfJoining { get; set; }
}
