namespace EmployeeManagementSystem.Model.Models.Auth;

public class RegisterModel
{
    [Required] public string? Name { get; set; }
    [Required, EmailAddress] public string? Email { get; set; }

    [Required, StringLength(100, MinimumLength = 6)]
    public string? Password { get; set; }

    [Required, Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }
}