﻿namespace EmployeeManagementSystem.Model.Models.Auth
{
    public class LoginModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(6), MaxLength(20)] 
        public string Password { get; set; }
    }
}
